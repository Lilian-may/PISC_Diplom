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
                string database = EnvLoader.Get("DB_NAME", "test");
                string user = EnvLoader.Get("DB_USER", "root");
                string password = EnvLoader.Get("DB_PASSWORD", "your_safety_password");

                connectionString = $"Server={host};Port={port};Database={database};Uid={user};Pwd={password};";

                // Проверка подключения
                TestConnection();

                initialized = true;
            }
            catch (Exception ex)
            {
                string errorMsg = $"Не удалось инициализировать подключение к базе данных.\n\n" +
                                  $"Инструкция:\n" +
                                  $"1. Проверьте файл .env в папке программы\n" +
                                  $"2. Убедитесь, что параметры подключения корректны\n" +
                                  $"3. Проверьте, что MySQL сервер запущен\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}\n\n" +
                                  $"Стек вызовов:\n{ex.StackTrace}";
                MessageBox.Show(errorMsg, "Ошибка базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditLogger.LogError("system", "DatabaseHelper.Initialize", ex);
                throw;
            }
        }

        private static void TestConnection()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                conn.Close();
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
                string errorMsg = $"Не удалось выполнить запрос к базе данных.\n\n" +
                                  $"Инструкция:\n" +
                                  $"1. Проверьте подключение к интернету (сервер {EnvLoader.Get("DB_HOST", "cfif31.ru")})\n" +
                                  $"2. Убедитесь, что сервер MySQL доступен\n" +
                                  $"3. Проверьте правильность параметров в файле .env\n\n" +
                                  $"Запрос: {Truncate(sql, 200)}\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}\n\n" +
                                  $"Стек вызовов:\n{ex.StackTrace}";
                MessageBox.Show(errorMsg, "Ошибка базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditLogger.LogError(Program.CurrentUser ?? "system", "ExecuteQuery", ex, sql);
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
                string errorMsg = $"Не удалось выполнить изменение данных в базе.\n\n" +
                                  $"Инструкция:\n" +
                                  $"1. Проверьте, что все поля заполнены корректно\n" +
                                  $"2. Убедитесь, что данные не дублируются\n" +
                                  $"3. Проверьте подключение к серверу\n\n" +
                                  $"Запрос: {Truncate(sql, 200)}\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}\n\n" +
                                  $"Стек вызовов:\n{ex.StackTrace}";
                MessageBox.Show(errorMsg, "Ошибка базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditLogger.LogError(Program.CurrentUser ?? "system", "ExecuteNonQuery", ex, sql);
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
                string errorMsg = $"Не удалось получить данные из базы.\n\n" +
                                  $"Инструкция:\n" +
                                  $"1. Проверьте подключение к серверу\n" +
                                  $"2. Убедитесь, что запрос составлен правильно\n\n" +
                                  $"Запрос: {Truncate(sql, 200)}\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}\n\n" +
                                  $"Стек вызовов:\n{ex.StackTrace}";
                MessageBox.Show(errorMsg, "Ошибка базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditLogger.LogError(Program.CurrentUser ?? "system", "ExecuteScalar", ex, sql);
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
            if (endIndex == -1) return text.Substring(startIndex).Trim();
            return text.Substring(startIndex, endIndex - startIndex).Trim();
        }

        private static string Truncate(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text)) return "";
            return text.Length <= maxLength ? text : text.Substring(0, maxLength) + "...";
        }
    }
}