#nullable disable
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Pipe
{
    public partial class LoginForm : Form
    {
        private int failedAttempts = 0;
        private DateTime? blockUntil = null;

        public LoginForm()
        {
            InitializeComponent();
            EnsureAdminUserExists();
        }

        private void EnsureAdminUserExists()
        {
            try
            {
                var count = DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM users");
                bool hasUsers = (count != null && Convert.ToInt32(count) > 0);
                btnCreateFirstUser.Visible = !hasUsers;
                if (!hasUsers)
                {
                    lblInfo.Text = "В базе нет пользователей. Введите логин и пароль и нажмите «Создать первого пользователя».";
                    lblInfo.ForeColor = Color.DarkOrange;
                }
                else
                {
                    lblInfo.Text = "Введите логин и пароль для входа.";
                    lblInfo.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = $"Не удалось проверить наличие пользователей в базе данных.\n\n" +
                                  $"Инструкция:\n" +
                                  $"1. Проверьте подключение к серверу MySQL\n" +
                                  $"2. Убедитесь, что база данных существует\n" +
                                  $"3. Проверьте параметры в файле .env\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}\n\n" +
                                  $"Стек вызовов:\n{ex.StackTrace}";

                MessageBox.Show(errorMsg, "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditLogger.LogError("system", "EnsureAdminUserExists", ex);
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (blockUntil.HasValue && DateTime.Now < blockUntil.Value)
                {
                    int secondsLeft = (int)(blockUntil.Value - DateTime.Now).TotalSeconds;
                    MessageBox.Show($"Слишком много неудачных попыток. Подождите {secondsLeft} секунд.", "Доступ заблокирован", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string login = txtLogin.Text.Trim();
                string password = txtPassword.Text;

                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Введите логин и пароль.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sql = "SELECT password_hash FROM users WHERE login=@login";
                var hashResult = DatabaseHelper.ExecuteScalar(sql, new MySqlParameter("@login", login));

                if (hashResult != null && SecurityHelper.VerifyPassword(password, hashResult.ToString()))
                {
                    Program.CurrentUser = login;
                    AuditLogger.Log(login, "Успешный вход в систему");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    failedAttempts++;
                    AuditLogger.Log(login, "Неудачная попытка входа", details: $"Осталось попыток: {3 - failedAttempts}");

                    if (failedAttempts >= 3)
                    {
                        blockUntil = DateTime.Now.AddSeconds(30);
                        MessageBox.Show("Три неудачные попытки входа. Доступ заблокирован на 30 секунд.", "Превышен лимит", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Неверный логин или пароль. Осталось попыток: {3 - failedAttempts}", "Ошибка аутентификации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                string errorMsg = $"Не удалось выполнить вход в систему.\n\n" +
                                  $"Инструкция:\n" +
                                  $"1. Проверьте подключение к базе данных\n" +
                                  $"2. Убедитесь, что учётная запись существует\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}\n\n" +
                                  $"Стек вызовов:\n{ex.StackTrace}";

                MessageBox.Show(errorMsg, "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditLogger.LogError(Program.CurrentUser ?? "unknown", "btnLogin_Click", ex);
            }
        }

        private void btnCreateFirstUser_Click(object sender, EventArgs e)
        {
            try
            {
                string login = txtLogin.Text.Trim();
                string password = txtPassword.Text;

                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Введите логин и пароль для создания первого пользователя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (password.Length < 6)
                {
                    MessageBox.Show("Пароль должен содержать не менее 6 символов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var count = DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM users");
                if (count != null && Convert.ToInt32(count) > 0)
                {
                    MessageBox.Show("Пользователи уже существуют. Создание первого пользователя невозможно.", "Действие запрещено", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnCreateFirstUser.Visible = false;
                    return;
                }

                string hash = SecurityHelper.HashPassword(password);
                string sql = "INSERT INTO users (login, password_hash) VALUES (@login, @hash)";
                DatabaseHelper.ExecuteNonQuery(sql,
                    new MySqlParameter("@login", login),
                    new MySqlParameter("@hash", hash));

                AuditLogger.Log(login, "Создан первый пользователь (Admin)");

                MessageBox.Show($"Пользователь '{login}' успешно создан. Теперь вы можете войти в систему.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnCreateFirstUser.Visible = false;
                lblInfo.Text = "Введите логин и пароль для входа.";
                lblInfo.ForeColor = Color.Gray;
                txtPassword.Clear();
                txtLogin.Clear();
            }
            catch (Exception ex)
            {
                string errorMsg = $"Не удалось создать пользователя.\n\n" +
                                  $"Инструкция:\n" +
                                  $"1. Возможно, логин уже существует\n" +
                                  $"2. Проверьте подключение к базе данных\n" +
                                  $"3. Пароль должен быть не менее 6 символов\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}\n\n" +
                                  $"Стек вызовов:\n{ex.StackTrace}";

                MessageBox.Show(errorMsg, "Ошибка создания пользователя", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditLogger.LogError("system", "btnCreateFirstUser_Click", ex);
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btnLogin_Click(sender, e);
        }
    }
}