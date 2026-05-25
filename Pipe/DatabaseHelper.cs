#nullable disable

using MySql.Data.MySqlClient;
using System.Data;

namespace Pipe
{
	public static class DatabaseHelper
	{
		private static string connectionString = "Server=cfif31.ru;Database=ISPr25-24_KuzminAO_gazprom_vtd;Uid=ISPr25-24_KuzminAO;Pwd=ISPr25-24_KuzminAO;";

		public static string GetConnectionString() => connectionString;

		public static DataTable ExecuteQuery(string sql, params MySqlParameter[] parameters)
		{
			using (var conn = new MySqlConnection(connectionString))
			using (var cmd = new MySqlCommand(sql, conn))
			{
				if (parameters != null) cmd.Parameters.AddRange(parameters);
				var da = new MySqlDataAdapter(cmd);
				var dt = new DataTable();
				da.Fill(dt);
				return dt;
			}
		}

		public static int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
		{
			using (var conn = new MySqlConnection(connectionString))
			using (var cmd = new MySqlCommand(sql, conn))
			{
				if (parameters != null) cmd.Parameters.AddRange(parameters);
				conn.Open();
				return cmd.ExecuteNonQuery();
			}
		}

		public static object ExecuteScalar(string sql, params MySqlParameter[] parameters)
		{
			using (var conn = new MySqlConnection(connectionString))
			using (var cmd = new MySqlCommand(sql, conn))
			{
				if (parameters != null) cmd.Parameters.AddRange(parameters);
				conn.Open();
				return cmd.ExecuteScalar();
			}
		}
	}
}