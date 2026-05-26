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
    public partial class ImportInspectionsForm : Form
    {
        public ImportInspectionsForm()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorWithDetails(
                    "Ошибка инициализации формы импорта.\n\nПроверьте установку и повторите попытку.",
                    "Ошибка",
                    ex);
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Excel files|*.xlsx;*.xls|CSV files|*.csv";
                    ofd.Title = "Выберите файл с инспекциями";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        txtFilePath.Text = ofd.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorWithDetails(
                    "Ошибка выбора файла.\n\nПроверьте путь к файлу и его доступность.",
                    "Ошибка",
                    ex);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text))
            {
                MessageBox.Show("Выберите файл для импорта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                DataTable dt = ReadFile(txtFilePath.Text);
                int imported = 0;
                int errors = 0;
                lblStatus.Text = "Импорт...";
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
                        System.Diagnostics.Debug.WriteLine($"Ошибка строки: {ex.Message}");
                    }
                }
                lblStatus.Text = $"Готово. Добавлено: {imported}, Ошибок: {errors}";
                MessageBox.Show($"Импорт завершён.\nДобавлено: {imported}\nОшибок: {errors}", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorWithDetails(
                    "Не удалось импортировать файл.\n\nПроверьте формат файла и структуру данных.",
                    "Ошибка импорта",
                    ex);
                lblStatus.Text = "Ошибка импорта";
            }
        }

        private DataTable ReadFile(string filePath)
        {
            DataTable dt = new DataTable();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            try
            {
                if (filePath.EndsWith(".csv"))
                {
                    var lines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8);
                    if (lines.Length == 0) return dt;
                    var headers = lines[0].Split(new char[] { ',', ';', '\t' });
                    foreach (var header in headers)
                        dt.Columns.Add(header.Trim());
                    for (int i = 1; i < lines.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(lines[i])) continue;
                        var values = lines[i].Split(new char[] { ',', ';', '\t' });
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
                                dataRow[col - 1] = worksheet.Cells[row, col].Text;
                            dt.Rows.Add(dataRow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorWithDetails(
                    "Ошибка чтения файла.\n\nПроверьте формат и содержимое файла.",
                    "Ошибка",
                    ex);
            }
            return dt;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}