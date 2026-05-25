#nullable disable
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Pipe
{
    public static class DatabaseHelper
    {
        private static string connectionString = "Server=localhost;Database=ISPr25-24_KuzminAO_gazprom_vtd;Uid=root;Pwd=;";

        public static string GetConnectionString() => connectionString;

        public static DataTable ExecuteQuery(string sql, params MySqlParameter[] parameters)
        {
            try
            {
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
                MessageBox.Show(
                    "Не удалось выполнить запрос к базе данных.\n\n" +
                    "Проверьте подключение к серверу MySQL и правильность строки подключения.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка базы данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return new DataTable();
            }
        }

        public static int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось выполнить изменение данных в базе.\n\n" +
                    "Проверьте, что все поля заполнены корректно, и данные не дублируются.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка базы данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return -1;
            }
        }

        public static object ExecuteScalar(string sql, params MySqlParameter[] parameters)
        {
            try
            {
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
                MessageBox.Show(
                    "Не удалось получить данные из базы.\n\n" +
                    "Проверьте подключение к серверу MySQL.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка базы данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
        }
    }
}