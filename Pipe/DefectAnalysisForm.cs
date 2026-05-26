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
        private DataView filteredView;
        private Pipeline currentPipeline;
        private int currentInspectionId = -1;
        private PipeCanvas pipeCanvas;
        private bool isFiltered = false;

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
                ShowError("Ошибка инициализации формы анализа дефектов", ex);
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
                btnResetView.Enabled = false;
                btnToggleGrid.Enabled = false;
            }
            catch (Exception ex)
            {
                ShowError("Ошибка инициализации графического контрола", ex);
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

                if (dt.Rows.Count == 0)
                {
                    lblPipeInfo.Text = "Нет трубопроводов в базе данных";
                }
            }
            catch (Exception ex)
            {
                ShowError("Не удалось загрузить список трубопроводов", ex);
            }
        }

        private void cmbPipeline_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbInspection.DataSource = null;
                cmbInspection.Items.Clear();
                cmbInspection.Text = "";
                cmbInspection.Enabled = false;
                currentInspectionId = -1;
                currentPipeline = null;
                isFiltered = false;

                if (defectsTable != null)
                {
                    defectsTable.Clear();
                    dataGridViewDefects.DataSource = null;
                }

                if (pipeCanvas != null)
                    pipeCanvas.SetDefects(new List<Defect>(), 0);

                btnImport.Enabled = false;
                btnFilterCritical.Enabled = false;
                btnResetFilter.Enabled = false;
                btnRecalc.Enabled = false;
                btnResetView.Enabled = false;
                btnToggleGrid.Enabled = false;
                lblProgress.Text = "";

                if (cmbPipeline.SelectedItem == null)
                {
                    lblPipeInfo.Text = "Выберите трубопровод";
                    return;
                }

                DataRowView selectedRow = cmbPipeline.SelectedItem as DataRowView;
                if (selectedRow == null)
                {
                    lblPipeInfo.Text = "Ошибка загрузки данных";
                    return;
                }

                int pid = Convert.ToInt32(selectedRow["id"]);
                LoadPipelineData(pid);
                LoadInspections(pid);
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при выборе трубопровода", ex);
            }
        }

        private void LoadPipelineData(int pid)
        {
            try
            {
                var pipeData = DatabaseHelper.ExecuteQuery("SELECT * FROM pipelines WHERE id=@id", new MySqlParameter("@id", pid));
                if (pipeData.Rows.Count > 0)
                {
                    var row = pipeData.Rows[0];
                    currentPipeline = new Pipeline
                    {
                        Id = pid,
                        Name = row["name"].ToString(),
                        DiameterMm = Convert.ToDouble(row["diameter_mm"]),
                        WallThicknessMm = Convert.ToDouble(row["wall_thickness_mm"]),
                        YieldStrengthMpa = Convert.ToDouble(row["yield_strength_mpa"]),
                        DesignPressureMpa = Convert.ToDouble(row["design_pressure_mpa"]),
                        OperatingPressureMpa = Convert.ToDouble(row["operating_pressure_mpa"])
                    };
                    lblPipeInfo.Text = $"{currentPipeline.Name} | D={currentPipeline.DiameterMm}мм | δ={currentPipeline.WallThicknessMm}мм | Pпр={currentPipeline.DesignPressureMpa} МПа | Pраб={currentPipeline.OperatingPressureMpa} МПа";
                }
            }
            catch (Exception ex)
            {
                ShowError("Ошибка загрузки данных трубопровода", ex);
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
                ShowError("Не удалось загрузить список инспекций", ex);
            }
        }

        private void cmbInspection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbInspection.SelectedItem == null)
                {
                    currentInspectionId = -1;
                    btnImport.Enabled = false;
                    btnFilterCritical.Enabled = false;
                    btnResetFilter.Enabled = false;
                    btnRecalc.Enabled = false;
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
                btnImport.Enabled = true;
                btnResetFilter.Enabled = true;
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при выборе инспекции", ex);
            }
        }

        private void LoadDefects()
        {
            if (currentInspectionId == -1 || currentPipeline == null) return;

            try
            {
                string sql = @"SELECT id, distance_m, angle_deg, defect_type, depth_percent, depth_mm, length_mm, width_mm,
                                      allowable_pressure_mpa, erf, severity
                               FROM defects WHERE inspection_id=@id ORDER BY distance_m";
                defectsTable = DatabaseHelper.ExecuteQuery(sql, new MySqlParameter("@id", currentInspectionId));

                // Сбрасываем фильтр
                isFiltered = false;
                dataGridViewDefects.DataSource = defectsTable;

                if (defectsTable.Rows.Count > 0)
                {
                    dataGridViewDefects.CellFormatting -= DataGridViewDefects_CellFormatting;
                    dataGridViewDefects.CellFormatting += DataGridViewDefects_CellFormatting;
                    dataGridViewDefects.AutoResizeColumns();

                    btnFilterCritical.Enabled = true;
                    btnRecalc.Enabled = true;
                    btnResetView.Enabled = true;
                    btnToggleGrid.Enabled = true;

                    lblProgress.Text = $"Загружено дефектов: {defectsTable.Rows.Count}";
                }
                else
                {
                    lblProgress.Text = "Нет дефектов для выбранной инспекции. Нажмите 'Импорт CSV' для загрузки.";
                    btnFilterCritical.Enabled = false;
                    btnRecalc.Enabled = false;
                }

                RefreshPipeCanvas();
            }
            catch (Exception ex)
            {
                ShowError("Не удалось загрузить дефекты", ex);
            }
        }

        private void DataGridViewDefects_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                DataTable currentTable = dataGridViewDefects.DataSource as DataTable;
                if (e.RowIndex >= 0 && currentTable != null && currentTable.Rows.Count > e.RowIndex)
                {
                    string sev = currentTable.Rows[e.RowIndex]["severity"]?.ToString();
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
                DataTable sourceTable = dataGridViewDefects.DataSource as DataTable ?? defectsTable;

                foreach (DataRow row in sourceTable.Rows)
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

        // ========== ОБРАБОТЧИКИ КНОПОК ==========

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
                        var progress = new Progress<string>(msg =>
                        {
                            lblProgress.Text = msg;
                            Application.DoEvents();
                        });
                        var result = Importer.ImportFromCsv(ofd.FileName, currentInspectionId, currentPipeline, progress);
                        MessageBox.Show($"Импорт завершён!\n\nОбработано: {result.TotalRows}\nДобавлено: {result.Added}\nПропущено: {result.Skipped}",
                            "Результат импорта", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDefects();
                    }
                    catch (Exception ex)
                    {
                        ShowError("Ошибка импорта файла", ex);
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
                    // Создаём фильтрованное представление
                    filteredView = new DataView(defectsTable);
                    filteredView.RowFilter = "severity = 'High' OR severity = 'Critical'";
                    dataGridViewDefects.DataSource = filteredView;
                    isFiltered = true;

                    int filteredCount = filteredView.Count;
                    lblProgress.Text = $"Фильтр: только High/Critical. Найдено: {filteredCount} из {defectsTable.Rows.Count}";

                    // Обновляем развёртку
                    RefreshPipeCanvas();

                    if (filteredCount == 0)
                    {
                        MessageBox.Show("Нет дефектов с категориями High или Critical", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Нет данных для фильтрации", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowError("Ошибка фильтрации", ex);
            }
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (defectsTable != null)
                {
                    dataGridViewDefects.DataSource = defectsTable;
                    isFiltered = false;
                    lblProgress.Text = $"Фильтр сброшен. Всего дефектов: {defectsTable.Rows.Count}";
                    RefreshPipeCanvas();
                }
                else
                {
                    MessageBox.Show("Нет данных для сброса фильтра", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowError("Ошибка сброса фильтра", ex);
            }
        }

        private void btnRecalc_Click(object sender, EventArgs e)
        {
            if (currentInspectionId == -1 || currentPipeline == null)
            {
                MessageBox.Show("Сначала выберите трубопровод и инспекцию!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (defectsTable == null || defectsTable.Rows.Count == 0)
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

                // Если был активен фильтр, переприменяем его
                if (isFiltered)
                {
                    filteredView = new DataView(defectsTable);
                    filteredView.RowFilter = "severity = 'High' OR severity = 'Critical'";
                    dataGridViewDefects.DataSource = filteredView;
                }

                RefreshPipeCanvas();
                dataGridViewDefects.Refresh();
                lblProgress.Text = $"Пересчёт завершён. Обновлено: {updated}";
            }
            catch (Exception ex)
            {
                ShowError("Ошибка пересчёта", ex);
            }
        }

        private void btnResetView_Click(object sender, EventArgs e)
        {
            try
            {
                pipeCanvas?.ResetView();
                lblProgress.Text = "Вид сброшен к исходному";
            }
            catch (Exception ex)
            {
                ShowError("Ошибка сброса вида", ex);
            }
        }

        private void btnToggleGrid_Click(object sender, EventArgs e)
        {
            try
            {
                pipeCanvas?.ToggleGrid();
                // Обновляем текст кнопки
                if (pipeCanvas != null)
                {
                    bool isGridVisible = pipeCanvas.IsGridVisible();
                    btnToggleGrid.Text = isGridVisible ? "Сетка: Выкл" : "Сетка: Вкл";
                }
                lblProgress.Text = "Сетка переключена";
            }
            catch (Exception ex)
            {
                ShowError("Ошибка переключения сетки", ex);
            }
        }

        private void ShowError(string message, Exception ex)
        {
            string errorMsg = $"{message}\n\n" +
                              $"Инструкция:\n" +
                              $"1. Проверьте подключение к базе данных\n" +
                              $"2. Убедитесь, что все необходимые таблицы существуют\n" +
                              $"3. Попробуйте перезапустить программу\n\n" +
                              $"Техническая ошибка:\n{ex.Message}\n\n" +
                              $"Стек вызовов:\n{ex.StackTrace}";
            MessageBox.Show(errorMsg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            AuditLogger.LogError(Program.CurrentUser, message, ex);
        }
    }
}