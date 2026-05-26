#nullable disable
using System;
using System.Windows.Forms;
using OfficeOpenXml;

namespace Pipe
{
    static class Program
    {
        public static string CurrentUser { get; set; }

        [STAThread]
        static void Main()
        {
            try
            {
                // Загрузка .env конфигурации
                EnvLoader.Load();

                // Инициализация базы данных
                DatabaseHelper.Initialize();

                // Настройка EPPlus
                ExcelPackage.License = LicenseContext.NonCommercial;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                using (var login = new LoginForm())
                {
                    if (login.ShowDialog() == DialogResult.OK)
                    {
                        AuditLogger.Log(CurrentUser, "Вход в систему");
                        Application.Run(new MainForm());
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = $"Критическая ошибка при запуске приложения.\n\n" +
                                  $"Инструкция:\n" +
                                  $"1. Убедитесь, что MySQL сервер запущен\n" +
                                  $"2. Проверьте файл .env в папке программы\n" +
                                  $"3. Убедитесь, что база данных существует\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}\n\n" +
                                  $"Стек вызовов:\n{ex.StackTrace}";

                MessageBox.Show(errorMsg, "Ошибка запуска", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}