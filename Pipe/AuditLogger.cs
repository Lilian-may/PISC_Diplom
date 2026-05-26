#nullable disable
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Pipe
{
    public static class AuditLogger
    {
        private static readonly object lockObj = new object();
        private static string logDirectory;
        private static bool enabled;

        static AuditLogger()
        {
            try
            {
                logDirectory = Path.Combine(Application.StartupPath, "Logs");
                if (!Directory.Exists(logDirectory))
                    Directory.CreateDirectory(logDirectory);

                enabled = EnvLoader.Get("AUDIT_LOG_ENABLED", "true").ToLower() == "true";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка инициализации логгера: {ex.Message}");
                enabled = false;
            }
        }

        public static void Log(string userLogin, string action, string tableName = null, int? recordId = null, string details = null)
        {
            if (!enabled) return;

            try
            {
                lock (lockObj)
                {
                    string logFile = Path.Combine(logDirectory, $"audit_{DateTime.Now:yyyyMMdd}.log");
                    var sb = new StringBuilder();
                    sb.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]");
                    sb.AppendLine($"  Пользователь: {Sanitize(userLogin)}");
                    sb.AppendLine($"  Действие: {Sanitize(action)}");
                    if (!string.IsNullOrEmpty(tableName))
                        sb.AppendLine($"  Таблица: {Sanitize(tableName)}");
                    if (recordId.HasValue)
                        sb.AppendLine($"  ID записи: {recordId.Value}");
                    if (!string.IsNullOrEmpty(details))
                        sb.AppendLine($"  Подробности: {Sanitize(details)}");
                    sb.AppendLine("---");

                    File.AppendAllText(logFile, sb.ToString(), Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка записи лога: {ex.Message}");
            }
        }

        public static void LogError(string userLogin, string action, Exception ex, string context = null)
        {
            if (!enabled) return;

            try
            {
                lock (lockObj)
                {
                    string logFile = Path.Combine(logDirectory, $"errors_{DateTime.Now:yyyyMMdd}.log");
                    var sb = new StringBuilder();
                    sb.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]");
                    sb.AppendLine($"  Пользователь: {Sanitize(userLogin)}");
                    sb.AppendLine($"  Действие: {Sanitize(action)}");
                    if (!string.IsNullOrEmpty(context))
                        sb.AppendLine($"  Контекст: {Sanitize(context)}");
                    sb.AppendLine($"  Ошибка: {ex.Message}");
                    sb.AppendLine($"  Стек: {ex.StackTrace}");
                    sb.AppendLine("---");

                    File.AppendAllText(logFile, sb.ToString(), Encoding.UTF8);
                }
            }
            catch (Exception logEx)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка записи лога ошибок: {logEx.Message}");
            }
        }

        private static string Sanitize(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            // Не храним пароли и чувствительные данные
            if (input.Contains("password", StringComparison.OrdinalIgnoreCase))
                return "[REDACTED]";
            return input;
        }
    }
}