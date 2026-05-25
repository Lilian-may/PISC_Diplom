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
    public partial class ImportPipelinesForm : Form
    {
        public ImportPipelinesForm()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка инициализации формы импорта: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Excel files|*.xlsx;*.xls|CSV files|*.csv";
                    ofd.Title = "Выберите файл с трубопроводами";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        txtFilePath.Text = ofd.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка выбора файла: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
                int updated = 0;
                int errors = 0;
                lblStatus.Text = "Импорт...";
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        string name = row["name"]?.ToString();
                        if (string.IsNullOrWhiteSpace(name)) continue;
                        double startKm = 0, endKm = 0, diameter = 0, wallThickness = 0, yieldStrength = 420, designPressure = 0, operatingPressure = 0;
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
                        if (exists > 0)
                        {
                            string sql = @"UPDATE pipelines SET start_km=@start, end_km=@end, diameter_mm=@diam, 
                                            wall_thickness_mm=@wall, steel_grade=@steel, yield_strength_mpa=@yield,
                                            design_pressure_mpa=@des, operating_pressure_mpa=@op, region=@region, 
                                            commissioning_date=@date WHERE name=@name";
                            DatabaseHelper.ExecuteNonQuery(sql,
                                new MySqlParameter("@name", name), new MySqlParameter("@start", startKm),
                                new MySqlParameter("@end", endKm), new MySqlParameter("@diam", diameter),
                                new MySqlParameter("@wall", wallThickness), new MySqlParameter("@steel", steelGrade),
                                new MySqlParameter("@yield", yieldStrength), new MySqlParameter("@des", designPressure),
                                new MySqlParameter("@op", operatingPressure), new MySqlParameter("@region", region ?? (object)DBNull.Value),
                                new MySqlParameter("@date", commissioningDate));
                            updated++;
                        }
                        else
                        {
                            string sql = @"INSERT INTO pipelines (name, start_km, end_km, diameter_mm, wall_thickness_mm, 
                                            steel_grade, yield_strength_mpa, design_pressure_mpa, operating_pressure_mpa, 
                                            region, commissioning_date) 
                                            VALUES (@name, @start, @end, @diam, @wall, @steel, @yield, @des, @op, @region, @date)";
                            DatabaseHelper.ExecuteNonQuery(sql,
                                new MySqlParameter("@name", name), new MySqlParameter("@start", startKm),
                                new MySqlParameter("@end", endKm), new MySqlParameter("@diam", diameter),
                                new MySqlParameter("@wall", wallThickness), new MySqlParameter("@steel", steelGrade),
                                new MySqlParameter("@yield", yieldStrength), new MySqlParameter("@des", designPressure),
                                new MySqlParameter("@op", operatingPressure), new MySqlParameter("@region", region ?? (object)DBNull.Value),
                                new MySqlParameter("@date", commissioningDate));
                            imported++;
                        }
                    }
                    catch (Exception ex)
                    {
                        errors++;
                        System.Diagnostics.Debug.WriteLine($"Ошибка строки: {ex.Message}");
                    }
                }
                lblStatus.Text = $"Готово. Добавлено: {imported}, Обновлено: {updated}, Ошибок: {errors}";
                MessageBox.Show($"Импорт завершён.\nДобавлено: {imported}\nОбновлено: {updated}\nОшибок: {errors}", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось импортировать файл.\n\n" +
                    "Проверьте формат файла и структуру данных.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка импорта",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
                MessageBox.Show(
                    $"Ошибка чтения файла: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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