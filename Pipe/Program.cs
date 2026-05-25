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
                ExcelPackage.License = LicenseContext.NonCommercial;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                using (var login = new LoginForm())
                {
                    if (login.ShowDialog() == DialogResult.OK)
                        Application.Run(new MainForm());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Критическая ошибка при запуске приложения.\n\n" +
                    "Проверьте установку необходимых компонентов и подключение к базе данных.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка запуска",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}