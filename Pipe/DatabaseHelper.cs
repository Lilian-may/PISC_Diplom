#nullable disable
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Pipe
{
    public static class DatabaseHelper
    {
        private static string connectionString;
        private static bool initialized = false;

        public static void Initialize()
        {
            if (initialized) return;

            try
            {
                string host = EnvLoader.Get("DB_HOST", "localhost");
                string port = EnvLoader.Get("DB_PORT", "3306");
                string database = EnvLoader.Get("DB_NAME", "ISPr25-24_KuzminAO_gazprom_vtd");
                string user = EnvLoader.Get("DB_USER", "root");
                string password = EnvLoader.Get("DB_PASSWORD", "");

                connectionString = $"Server={host};Port={port};Database={database};Uid={user};Pwd={password};";
                initialized = true;

                // Проверка подключения
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                ShowError("Не удалось подключиться к базе данных",
                    "Проверьте:\n" +
                    "• Запущен ли сервер MySQL\n" +
                    "• Правильность параметров в файле .env (DB_HOST, DB_PORT, DB_NAME, DB_USER, DB_PASSWORD)\n" +
                    "• Доступность сети и настройки брандмауэра",
                    ex);
                throw;
            }
        }

        public static string GetConnectionString() => connectionString;

        public static DataTable ExecuteQuery(string sql, params MySqlParameter[] parameters)
        {
            try
            {
                Initialize();
                using (var conn = new MySqlConnection(connectionString))
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    var da = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                AuditLogger.LogError(Program.CurrentUser ?? "system", "ExecuteQuery", ex, sql);
                ShowError("Не удалось выполнить запрос к базе данных",
                    "Проверьте подключение к серверу MySQL и правильность SQL-запроса.\n\n" +
                    "Если ошибка повторяется, обратитесь к администратору.",
                    ex);
                return new DataTable();
            }
        }

        public static int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            try
            {
                Initialize();
                using (var conn = new MySqlConnection(connectionString))
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    AuditLogger.Log(Program.CurrentUser ?? "system", "ExecuteNonQuery", GetTableName(sql), null, $"Запрос: {Truncate(sql, 200)}");
                    return result;
                }
            }
            catch (Exception ex)
            {
                AuditLogger.LogError(Program.CurrentUser ?? "system", "ExecuteNonQuery", ex, sql);
                ShowError("Не удалось выполнить изменение данных в базе",
                    "Проверьте, что все поля заполнены корректно, и данные не дублируются.\n\n" +
                    "Убедитесь, что у вас есть права на запись.",
                    ex);
                return -1;
            }
        }

        public static object ExecuteScalar(string sql, params MySqlParameter[] parameters)
        {
            try
            {
                Initialize();
                using (var conn = new MySqlConnection(connectionString))
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                AuditLogger.LogError(Program.CurrentUser ?? "system", "ExecuteScalar", ex, sql);
                ShowError("Не удалось получить данные из базы",
                    "Проверьте подключение к серверу MySQL и правильность запроса.",
                    ex);
                return null;
            }
        }

        private static string GetTableName(string sql)
        {
            try
            {
                string upper = sql.ToUpper();
                if (upper.Contains("INSERT INTO"))
                    return ExtractBetween(upper, "INSERT INTO", "(");
                if (upper.Contains("UPDATE"))
                    return ExtractBetween(upper, "UPDATE", "SET");
                if (upper.Contains("DELETE FROM"))
                    return ExtractBetween(upper, "DELETE FROM", "WHERE");
                if (upper.Contains("SELECT"))
                    return ExtractBetween(upper, "SELECT", "FROM");
                return "unknown";
            }
            catch { return "unknown"; }
        }

        private static string ExtractBetween(string text, string start, string end)
        {
            int startIndex = text.IndexOf(start) + start.Length;
            int endIndex = text.IndexOf(end, startIndex);
            return text.Substring(startIndex, endIndex - startIndex).Trim();
        }

        private static string Truncate(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text)) return "";
            return text.Length <= maxLength ? text : text.Substring(0, maxLength) + "...";
        }

        private static void ShowError(string userMessage, string instruction, Exception ex)
        {
            string fullMessage = $"{userMessage}\n\n" +
                                 $"Инструкция:\n{instruction}\n\n" +
                                 $"Техническая ошибка:\n{ex.Message}\n\n" +
                                 $"Стек вызовов:\n{ex.StackTrace}";

            var result = MessageBox.Show(fullMessage, "Ошибка базы данных", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            if (result == DialogResult.Abort)
                Environment.Exit(1);
        }
    }
}