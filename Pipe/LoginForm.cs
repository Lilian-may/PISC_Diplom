#nullable disable

using MySql.Data.MySqlClient;
using Pipe;
using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Pipe
{
    public partial class LoginForm : Form
    {
        private int failedAttempts = 0;
        private DateTime? blockUntil = null;

        public LoginForm()
        {
            InitializeComponent();
            CheckAndSetupFirstUserButton();
        }

        private void CheckAndSetupFirstUserButton()
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
                MessageBox.Show($"Ошибка при проверке пользователей: {ex.Message}", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                this.Close();
            }
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
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

            string hash = ComputeSha256Hash(password);
            string sql = "SELECT id FROM users WHERE login=@login AND password_hash=@hash";
            var result = DatabaseHelper.ExecuteScalar(sql,
                new MySqlParameter("@login", login),
                new MySqlParameter("@hash", hash));

            if (result != null)
            {
                Program.CurrentUser = login;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                failedAttempts++;
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

        private void btnCreateFirstUser_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль для создания первого пользователя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var count = DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM users");
            if (count != null && Convert.ToInt32(count) > 0)
            {
                MessageBox.Show("Пользователи уже существуют. Создание первого пользователя невозможно.", "Действие запрещено", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnCreateFirstUser.Visible = false;
                return;
            }

            string hash = ComputeSha256Hash(password);
            try
            {
                string sql = "INSERT INTO users (login, password_hash) VALUES (@login, @hash)";
                DatabaseHelper.ExecuteNonQuery(sql,
                    new MySqlParameter("@login", login),
                    new MySqlParameter("@hash", hash));
                MessageBox.Show($"Пользователь '{login}' успешно создан. Теперь вы можете войти в систему.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCreateFirstUser.Visible = false;
                lblInfo.Text = "Введите логин и пароль для входа.";
                lblInfo.ForeColor = Color.Gray;
                txtPassword.Clear();
                txtLogin.Clear();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                    MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show($"Ошибка БД: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btnLogin_Click(sender, e);
        }
    }
}