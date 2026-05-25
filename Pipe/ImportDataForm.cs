#nullable disable
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OfficeOpenXml;

namespace Pipe
{
    public partial class ImportDataForm : Form
    {
        public ImportDataForm()
        {
            InitializeComponent();
        }

        private void btnImportPipelines_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel files|*.xlsx;*.xls|CSV files|*.csv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DataTable dt = ReadExcelFile(ofd.FileName);
                        int imported = 0;
                        int errors = 0;

                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                string name = row["name"]?.ToString();
                                if (string.IsNullOrWhiteSpace(name)) continue;

                                double startKm = 0;
                                double endKm = 0;
                                double diameter = 0;
                                double wallThickness = 0;
                                double yieldStrength = 420;
                                double designPressure = 0;
                                double operatingPressure = 0;

                                double.TryParse(row["start_km"]?.ToString(), out startKm);
                                double.TryParse(row["end_km"]?.ToString(), out endKm);
                                double.TryParse(row["diameter_mm"]?.ToString(), out diameter);
                                double.TryParse(row["wall_thickness_mm"]?.ToString(), out wallThickness);
                                double.TryParse(row["yield_strength_mpa"]?.ToString(), out yieldStrength);
                                double.TryParse(row["design_pressure_mpa"]?.ToString(), out designPressure);
                                double.TryParse(row["operating_pressure_mpa"]?.ToString(), out operatingPressure);

                                string steelGrade = row["steel_grade"]?.ToString() ?? "К60";
                                string region = row["region"]?.ToString();
                                DateTime commissioningDate = DateTime.TryParse(row["commissioning_date"]?.ToString(), out var date) ? date : DateTime.Now;

                                string checkSql = "SELECT COUNT(*) FROM pipelines WHERE name = @name";
                                var exists = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkSql, new MySqlParameter("@name", name)));

                                string sql;
                                if (exists > 0)
                                {
                                    sql = @"UPDATE pipelines SET start_km=@start, end_km=@end, diameter_mm=@diam, 
                                            wall_thickness_mm=@wall, steel_grade=@steel, yield_strength_mpa=@yield,
                                            design_pressure_mpa=@des, operating_pressure_mpa=@op, region=@region, 
                                            commissioning_date=@date WHERE name=@name";
                                }
                                else
                                {
                                    sql = @"INSERT INTO pipelines (name, start_km, end_km, diameter_mm, wall_thickness_mm, 
                                            steel_grade, yield_strength_mpa, design_pressure_mpa, operating_pressure_mpa, 
                                            region, commissioning_date) 
                                            VALUES (@name, @start, @end, @diam, @wall, @steel, @yield, @des, @op, @region, @date)";
                                }

                                DatabaseHelper.ExecuteNonQuery(sql,
                                    new MySqlParameter("@name", name),
                                    new MySqlParameter("@start", startKm),
                                    new MySqlParameter("@end", endKm),
                                    new MySqlParameter("@diam", diameter),
                                    new MySqlParameter("@wall", wallThickness),
                                    new MySqlParameter("@steel", steelGrade),
                                    new MySqlParameter("@yield", yieldStrength),
                                    new MySqlParameter("@des", designPressure),
                                    new MySqlParameter("@op", operatingPressure),
                                    new MySqlParameter("@region", region ?? (object)DBNull.Value),
                                    new MySqlParameter("@date", commissioningDate));
                                imported++;
                            }
                            catch (Exception ex)
                            {
                                errors++;
                                System.Diagnostics.Debug.WriteLine($"Ошибка: {ex.Message}");
                            }
                        }
                        MessageBox.Show($"Импорт трубопроводов завершён.\nДобавлено/обновлено: {imported}\nОшибок: {errors}", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при импорте: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnImportInspections_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel files|*.xlsx;*.xls|CSV files|*.csv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DataTable dt = ReadExcelFile(ofd.FileName);
                        int imported = 0;
                        int errors = 0;

                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                string pipelineName = row["pipeline_name"]?.ToString();
                                if (string.IsNullOrWhiteSpace(pipelineName)) continue;

                                var pipelineIdResult = DatabaseHelper.ExecuteScalar("SELECT id FROM pipelines WHERE name = @name", new MySqlParameter("@name", pipelineName));
                                if (pipelineIdResult == null) continue;
                                int pipelineId = Convert.ToInt32(pipelineIdResult);

                                DateTime inspectionDate = DateTime.TryParse(row["inspection_date"]?.ToString(), out var date) ? date : DateTime.Now;
                                string toolType = row["tool_type"]?.ToString();
                                if (toolType != "MFL" && toolType != "UT" && toolType != "COMBO") toolType = "MFL";

                                double speed = 0;
                                double.TryParse(row["speed_mps"]?.ToString(), out speed);
                                int coverage = int.TryParse(row["coverage_percent"]?.ToString(), out int cov) ? cov : 100;
                                string status = row["status"]?.ToString() ?? "Выполнена";

                                string sql = @"INSERT INTO inspections (pipeline_id, inspection_date, tool_type, speed_mps, coverage_percent, status)
                                               VALUES (@pid, @date, @type, @speed, @cov, @status)";

                                DatabaseHelper.ExecuteNonQuery(sql,
                                    new MySqlParameter("@pid", pipelineId),
                                    new MySqlParameter("@date", inspectionDate),
                                    new MySqlParameter("@type", toolType),
                                    new MySqlParameter("@speed", speed == 0 ? DBNull.Value : (object)speed),
                                    new MySqlParameter("@cov", coverage),
                                    new MySqlParameter("@status", status));
                                imported++;
                            }
                            catch (Exception ex)
                            {
                                errors++;
                                System.Diagnostics.Debug.WriteLine($"Ошибка: {ex.Message}");
                            }
                        }
                        MessageBox.Show($"Импорт инспекций завершён.\nДобавлено: {imported}\nОшибок: {errors}", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при импорте: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private DataTable ReadExcelFile(string filePath)
        {
            DataTable dt = new DataTable();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            if (filePath.EndsWith(".csv"))
            {
                var lines = File.ReadAllLines(filePath);
                if (lines.Length == 0) return dt;
                var headers = lines[0].Split(',', ';', '\t');
                foreach (var header in headers)
                    dt.Columns.Add(header.Trim());

                for (int i = 1; i < lines.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lines[i])) continue;
                    var values = lines[i].Split(',', ';', '\t');
                    if (values.Length >= dt.Columns.Count)
                        dt.Rows.Add(values.Take(dt.Columns.Count).ToArray());
                }
            }
            else
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int col = 1; col <= colCount; col++)
                    {
                        string colName = worksheet.Cells[1, col].Text;
                        if (string.IsNullOrEmpty(colName)) colName = $"Column{col}";
                        dt.Columns.Add(colName);
                    }

                    for (int row = 2; row <= rowCount; row++)
                    {
                        DataRow dataRow = dt.NewRow();
                        for (int col = 1; col <= colCount; col++)
                        {
                            dataRow[col - 1] = worksheet.Cells[row, col].Text;
                        }
                        dt.Rows.Add(dataRow);
                    }
                }
            }
            return dt;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RefreshData()
        {
        }
    }
}