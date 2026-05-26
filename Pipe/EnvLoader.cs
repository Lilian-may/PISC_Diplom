#nullable disable
using System;
using System.IO;
using System.Windows.Forms;

namespace Pipe
{
    public static class EnvLoader
    {
        private static string envFilePath = Path.Combine(Application.StartupPath, ".env");

        public static void Load()
        {
            try
            {
                if (!File.Exists(envFilePath))
                {
                    CreateDefaultEnvFile();
                }

                foreach (var line in File.ReadAllLines(envFilePath))
                {
                    if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                        continue;

                    var parts = line.Split('=', 2);
                    if (parts.Length == 2)
                    {
                        var key = parts[0].Trim();
                        var value = parts[1].Trim();
                        Environment.SetEnvironmentVariable(key, value);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Ошибка загрузки конфигурационного файла .env",
                    "Убедитесь, что файл .env существует в папке программы и имеет правильный формат.\n\n" +
                    "Файл должен содержать строки вида: KEY=VALUE\n\n" +
                    "Обязательные параметры:\n" +
                    "DB_HOST=localhost\n" +
                    "DB_NAME=test_db_name\n" +
                    "DB_USER=root\n" +
                    "DB_PASSWORD=examp_pass",
                    ex);
            }
        }

        private static void CreateDefaultEnvFile()
        {
            string defaultContent = @"# Конфигурация подключения к базе данных
                DB_HOST=localhost
                DB_PORT=3306
                DB_NAME=test_db_name
                DB_USER=root
                DB_PASSWORD=examp_pass

                # Настройки приложения
                LOG_LEVEL=Info
                AUDIT_LOG_ENABLED=true
            ";
            File.WriteAllText(envFilePath, defaultContent);
            MessageBox.Show(
                "Создан файл конфигурации .env\n\n" +
                "Пожалуйста, отредактируйте его, указав правильные параметры подключения к базе данных.\n" +
                "Файл находится в папке с программой.\n\n" +
                "После редактирования перезапустите приложение.",
                "Первый запуск",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            Environment.Exit(0);
        }

        public static string Get(string key, string defaultValue = "")
        {
            return Environment.GetEnvironmentVariable(key) ?? defaultValue;
        }

        private static void ShowError(string userMessage, string instruction, Exception ex)
        {
            string fullMessage = $"{userMessage}\n\n" +
                                 $"Инструкция:\n{instruction}\n\n" +
                                 $"Техническая ошибка:\n{ex.Message}\n\n" +
                                 $"Стек вызовов:\n{ex.StackTrace}";

            var result = MessageBox.Show(fullMessage, "Ошибка", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            if (result == DialogResult.Abort)
                Environment.Exit(1);
        }
    }
}