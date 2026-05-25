#nullable disable
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Pipe
{
    public partial class DefectAnalysisForm : Form
    {
        private DataTable defectsTable;
        private Pipeline currentPipeline;
        private int currentInspectionId = -1;
        private PipeCanvas pipeCanvas;

        public DefectAnalysisForm()
        {
            try
            {
                InitializeComponent();
                InitializeCustomControls();
                LoadPipelines();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка инициализации формы анализа дефектов: {ex.Message}",
                    "Критическая ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void InitializeCustomControls()
        {
            try
            {
                pipeCanvas = new PipeCanvas();
                pipeCanvas.Dock = DockStyle.Fill;
                panelCanvas.Controls.Add(pipeCanvas);
                btnImport.Enabled = false;
                btnFilterCritical.Enabled = false;
                btnResetFilter.Enabled = false;
                btnRecalc.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка инициализации графического контрола: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadPipelines()
        {
            try
            {
                var dt = DatabaseHelper.ExecuteQuery("SELECT id, name FROM pipelines ORDER BY name");
                cmbPipeline.DataSource = dt;
                cmbPipeline.DisplayMember = "name";
                cmbPipeline.ValueMember = "id";
                cmbPipeline.SelectedIndex = -1;
                ClearInspectionCombo();
                ClearDefectsDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось загрузить список трубопроводов.\n\n" +
                    "Проверьте подключение к базе данных.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка загрузки",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearInspectionCombo()
        {
            try
            {
                cmbInspection.DataSource = null;
                cmbInspection.Items.Clear();
                cmbInspection.Enabled = false;
                currentInspectionId = -1;
                currentPipeline = null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ClearInspectionCombo error: {ex.Message}");
            }
        }

        private void ClearDefectsDisplay()
        {
            try
            {
                if (defectsTable != null)
                {
                    defectsTable.Clear();
                    defectsTable = null;
                }
                dataGridViewDefects.DataSource = null;
                lblPipeInfo.Text = "Выберите трубопровод";
                lblProgress.Text = "Нет данных";
                if (pipeCanvas != null)
                    pipeCanvas.SetDefects(new List<Defect>(), 0);
                btnImport.Enabled = false;
                btnFilterCritical.Enabled = false;
                btnResetFilter.Enabled = false;
                btnRecalc.Enabled = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ClearDefectsDisplay error: {ex.Message}");
            }
        }

        private void cmbPipeline_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClearInspectionCombo();
                ClearDefectsDisplay();
                if (cmbPipeline.SelectedItem == null) return;
                DataRowView selectedRow = cmbPipeline.SelectedItem as DataRowView;
                if (selectedRow == null) return;
                int pid = Convert.ToInt32(selectedRow["id"]);
                LoadPipelineData(pid);
                LoadInspections(pid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка при выборе трубопровода: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadPipelineData(int pid)
        {
            try
            {
                var pipeData = DatabaseHelper.ExecuteQuery("SELECT * FROM pipelines WHERE id=@id", new MySqlParameter("@id", pid));
                if (pipeData.Rows.Count > 0)
                {
                    currentPipeline = new Pipeline
                    {
                        Id = pid,
                        Name = pipeData.Rows[0]["name"].ToString(),
                        DiameterMm = Convert.ToDouble(pipeData.Rows[0]["diameter_mm"]),
                        WallThicknessMm = Convert.ToDouble(pipeData.Rows[0]["wall_thickness_mm"]),
                        YieldStrengthMpa = Convert.ToDouble(pipeData.Rows[0]["yield_strength_mpa"]),
                        DesignPressureMpa = Convert.ToDouble(pipeData.Rows[0]["design_pressure_mpa"]),
                        OperatingPressureMpa = Convert.ToDouble(pipeData.Rows[0]["operating_pressure_mpa"])
                    };
                    lblPipeInfo.Text = $"{currentPipeline.Name} | D={currentPipeline.DiameterMm}мм | δ={currentPipeline.WallThicknessMm}мм | Pпр={currentPipeline.DesignPressureMpa} МПа | Pраб={currentPipeline.OperatingPressureMpa} МПа";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка загрузки данных трубопровода: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadInspections(int pipelineId)
        {
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(
                    "SELECT id, inspection_date, tool_type FROM inspections WHERE pipeline_id=@pid ORDER BY inspection_date DESC",
                    new MySqlParameter("@pid", pipelineId));
                if (dt.Rows.Count > 0)
                {
                    cmbInspection.DataSource = dt;
                    cmbInspection.DisplayMember = "inspection_date";
                    cmbInspection.ValueMember = "id";
                    cmbInspection.Enabled = true;
                    lblProgress.Text = $"Выберите инспекцию ({dt.Rows.Count} доступно)";
                }
                else
                {
                    cmbInspection.Enabled = false;
                    lblProgress.Text = "Нет инспекций для выбранного трубопровода";
                    btnImport.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось загрузить список инспекций.\n\n" +
                    "Проверьте подключение к базе данных.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка загрузки",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void cmbInspection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbInspection.SelectedItem == null)
                {
                    currentInspectionId = -1;
                    ClearDefectsDisplay();
                    return;
                }
                DataRowView selectedRow = cmbInspection.SelectedItem as DataRowView;
                if (selectedRow == null)
                {
                    currentInspectionId = -1;
                    return;
                }
                currentInspectionId = Convert.ToInt32(selectedRow["id"]);
                LoadDefects();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка при выборе инспекции: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadDefects()
        {
            if (currentInspectionId == -1 || currentPipeline == null)
            {
                ClearDefectsDisplay();
                return;
            }
            try
            {
                string sql = @"SELECT id, distance_m, angle_deg, defect_type, depth_percent, depth_mm, length_mm, width_mm,
                                      allowable_pressure_mpa, erf, severity
                               FROM defects WHERE inspection_id=@id ORDER BY distance_m";
                defectsTable = DatabaseHelper.ExecuteQuery(sql, new MySqlParameter("@id", currentInspectionId));
                dataGridViewDefects.DataSource = defectsTable;
                if (defectsTable.Rows.Count > 0)
                {
                    dataGridViewDefects.CellFormatting -= dataGridViewDefects_CellFormatting;
                    dataGridViewDefects.CellFormatting += dataGridViewDefects_CellFormatting;
                    dataGridViewDefects.AutoResizeColumns();
                    btnImport.Enabled = true;
                    btnFilterCritical.Enabled = true;
                    btnResetFilter.Enabled = true;
                    btnRecalc.Enabled = true;
                    lblProgress.Text = $"Загружено дефектов: {defectsTable.Rows.Count}";
                }
                else
                {
                    lblProgress.Text = "Нет дефектов для выбранной инспекции. Нажмите 'Импорт CSV' для загрузки.";
                    btnImport.Enabled = true;
                    btnFilterCritical.Enabled = false;
                    btnResetFilter.Enabled = false;
                    btnRecalc.Enabled = false;
                }
                RefreshPipeCanvas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось загрузить дефекты.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка загрузки",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                ClearDefectsDisplay();
            }
        }

        private void dataGridViewDefects_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && defectsTable != null && defectsTable.Rows.Count > e.RowIndex)
                {
                    string sev = defectsTable.Rows[e.RowIndex]["severity"]?.ToString();
                    if (sev == "Critical")
                        e.CellStyle.BackColor = Color.LightCoral;
                    else if (sev == "High")
                        e.CellStyle.BackColor = Color.Orange;
                    else if (sev == "Medium")
                        e.CellStyle.BackColor = Color.LightYellow;
                    else if (sev == "Low")
                        e.CellStyle.BackColor = Color.LightGreen;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CellFormatting error: {ex.Message}");
            }
        }

        private void RefreshPipeCanvas()
        {
            try
            {
                if (pipeCanvas == null || currentPipeline == null || defectsTable == null || defectsTable.Rows.Count == 0)
                {
                    pipeCanvas?.SetDefects(new List<Defect>(), 0);
                    return;
                }
                var list = new List<Defect>();
                foreach (DataRow row in defectsTable.Rows)
                {
                    list.Add(new Defect
                    {
                        DistanceM = Convert.ToDouble(row["distance_m"]),
                        AngleDeg = Convert.ToInt32(row["angle_deg"]),
                        Severity = row["severity"]?.ToString(),
                        LengthMm = Convert.ToDouble(row["length_mm"]),
                        WidthMm = Convert.ToDouble(row["width_mm"]),
                        DefectType = row["defect_type"]?.ToString(),
                        DepthPercent = Convert.ToDouble(row["depth_percent"]),
                        Erf = row["erf"] == DBNull.Value ? 0 : Convert.ToDouble(row["erf"]),
                        AllowablePressureMpa = row["allowable_pressure_mpa"] == DBNull.Value ? 0 : Convert.ToDouble(row["allowable_pressure_mpa"])
                    });
                }
                pipeCanvas.SetDefects(list, currentPipeline.DiameterMm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"RefreshPipeCanvas error: {ex.Message}");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (currentInspectionId == -1 || currentPipeline == null)
            {
                MessageBox.Show("Сначала выберите трубопровод и инспекцию!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                ofd.Title = "Выберите файл с дефектами";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var progress = new Progress<string>(msg => lblProgress.Text = msg);
                        var result = Importer.ImportFromCsv(ofd.FileName, currentInspectionId, currentPipeline, progress);
                        MessageBox.Show($"Импорт завершён!\n\nОбработано: {result.TotalRows}\nДобавлено: {result.Added}\nПропущено: {result.Skipped}",
                            "Результат импорта", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDefects();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"Ошибка импорта: {ex.Message}",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        lblProgress.Text = "Ошибка импорта";
                    }
                }
            }
        }

        private void btnFilterCritical_Click(object sender, EventArgs e)
        {
            try
            {
                if (defectsTable != null && defectsTable.Rows.Count > 0)
                {
                    var dv = defectsTable.DefaultView;
                    dv.RowFilter = "severity IN ('High','Critical')";
                    dataGridViewDefects.DataSource = dv;
                    lblProgress.Text = $"Фильтр: только High/Critical. Всего: {dv.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка фильтрации: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (defectsTable != null)
                {
                    dataGridViewDefects.DataSource = defectsTable;
                    lblProgress.Text = $"Фильтр сброшен. Всего дефектов: {defectsTable.Rows.Count}";
                    RefreshPipeCanvas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка сброса фильтра: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnRecalc_Click(object sender, EventArgs e)
        {
            if (currentInspectionId == -1 || currentPipeline == null || defectsTable == null || defectsTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для пересчёта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int updated = 0;
                string updateSql = "UPDATE defects SET allowable_pressure_mpa=@allow, erf=@erf, severity=@sev WHERE id=@id";
                foreach (DataRow row in defectsTable.Rows)
                {
                    double depthPct = Convert.ToDouble(row["depth_percent"]);
                    double length = Convert.ToDouble(row["length_mm"]);
                    double width = Convert.ToDouble(row["width_mm"]);
                    var calc = StrengthCalculator.Calculate(currentPipeline, depthPct, length, width);
                    row["allowable_pressure_mpa"] = calc.allowablePressure;
                    row["erf"] = calc.erf;
                    row["severity"] = calc.severity;
                    DatabaseHelper.ExecuteNonQuery(updateSql,
                        new MySqlParameter("@allow", calc.allowablePressure),
                        new MySqlParameter("@erf", calc.erf),
                        new MySqlParameter("@sev", calc.severity),
                        new MySqlParameter("@id", Convert.ToInt32(row["id"])));
                    updated++;
                }
                MessageBox.Show($"Пересчёт завершён. Обновлено дефектов: {updated}", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshPipeCanvas();
                dataGridViewDefects.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка пересчёта: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}