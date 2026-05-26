#nullable disable
using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Pipe
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            try
            {
                InitializeComponent();
                CenterToParent(); // Центрируем форму относительно родителя
                LoadPipelines();
            }
            catch (Exception ex)
            {
                ShowError("Ошибка инициализации формы отчётов", ex);
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
                    cmbPipeline.Enabled = false;
                    cmbInspection.Enabled = false;
                    btnGeneratePdf.Enabled = false;
                    btnExportExcel.Enabled = false;
                    lblStatus.Text = "Нет трубопроводов в базе данных";
                }
                else
                {
                    lblStatus.Text = "Выберите трубопровод";
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
                // Очищаем список инспекций
                cmbInspection.DataSource = null;
                cmbInspection.Items.Clear();
                cmbInspection.Text = "";
                cmbInspection.Enabled = false;
                btnGeneratePdf.Enabled = false;
                btnExportExcel.Enabled = false;

                if (cmbPipeline.SelectedItem == null)
                {
                    lblStatus.Text = "Выберите трубопровод";
                    return;
                }

                DataRowView selectedRow = cmbPipeline.SelectedItem as DataRowView;
                if (selectedRow == null)
                {
                    lblStatus.Text = "Ошибка загрузки данных";
                    return;
                }

                int pid = Convert.ToInt32(selectedRow["id"]);
                string pipelineName = selectedRow["name"].ToString();
                LoadInspections(pid, pipelineName);
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при выборе трубопровода", ex);
            }
        }

        private void LoadInspections(int pipelineId, string pipelineName)
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
                    lblStatus.Text = $"Трубопровод: {pipelineName} | Найдено инспекций: {dt.Rows.Count}";
                }
                else
                {
                    cmbInspection.Enabled = false;
                    lblStatus.Text = $"Трубопровод: {pipelineName} | Нет инспекций";
                    btnGeneratePdf.Enabled = false;
                    btnExportExcel.Enabled = false;
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
                    btnGeneratePdf.Enabled = false;
                    btnExportExcel.Enabled = false;
                    return;
                }

                DataRowView selectedRow = cmbInspection.SelectedItem as DataRowView;
                if (selectedRow == null)
                {
                    btnGeneratePdf.Enabled = false;
                    btnExportExcel.Enabled = false;
                    return;
                }

                int inspId = Convert.ToInt32(selectedRow["id"]);
                string inspDate = selectedRow["inspection_date"].ToString();

                btnGeneratePdf.Enabled = true;
                btnExportExcel.Enabled = true;
                lblStatus.Text = $"Выбрана инспекция от {inspDate}";
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при выборе инспекции", ex);
            }
        }

        private void btnGeneratePdf_Click(object sender, EventArgs e)
        {
            if (cmbPipeline.SelectedItem == null || cmbInspection.SelectedItem == null)
            {
                MessageBox.Show("Сначала выберите трубопровод и инспекцию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataRowView pipeRow = cmbPipeline.SelectedItem as DataRowView;
                if (pipeRow == null) return;
                int pipeId = Convert.ToInt32(pipeRow["id"]);

                DataRowView inspRow = cmbInspection.SelectedItem as DataRowView;
                if (inspRow == null) return;
                int inspId = Convert.ToInt32(inspRow["id"]);

                // Загружаем критические дефекты
                DataTable defects = DatabaseHelper.ExecuteQuery(
                    "SELECT * FROM defects WHERE inspection_id=@id AND severity IN ('High','Critical') ORDER BY erf DESC",
                    new MySqlParameter("@id", inspId));

                if (defects.Rows.Count == 0)
                {
                    MessageBox.Show("Нет критических дефектов (High/Critical) для выбранной инспекции",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataTable pipeData = DatabaseHelper.ExecuteQuery("SELECT * FROM pipelines WHERE id=@id", new MySqlParameter("@id", pipeId));
                DataTable inspData = DatabaseHelper.ExecuteQuery("SELECT * FROM inspections WHERE id=@id", new MySqlParameter("@id", inspId));

                if (pipeData.Rows.Count == 0 || inspData.Rows.Count == 0)
                {
                    MessageBox.Show("Данные не найдены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Pipeline p = new Pipeline
                {
                    Name = pipeData.Rows[0]["name"].ToString(),
                    DesignPressureMpa = Convert.ToDouble(pipeData.Rows[0]["design_pressure_mpa"]),
                    OperatingPressureMpa = Convert.ToDouble(pipeData.Rows[0]["operating_pressure_mpa"])
                };

                Inspection i = new Inspection
                {
                    Date = Convert.ToDateTime(inspData.Rows[0]["inspection_date"]),
                    ToolType = inspData.Rows[0]["tool_type"].ToString()
                };

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF files|*.pdf";
                sfd.FileName = $"Отчет_ВТД_{p.Name}_{i.Date:yyyyMMdd}.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ReportGenerator.GenerateCriticalDefectsPdf(defects, p, i, sfd.FileName);
                    MessageBox.Show($"PDF-отчёт сохранён:\n{sfd.FileName}", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AuditLogger.Log(Program.CurrentUser, "Экспорт PDF отчёта", details: $"Трубопровод: {p.Name}, Инспекция: {i.Date:dd.MM.yyyy}");
                }
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при генерации PDF-отчёта", ex);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (cmbPipeline.SelectedItem == null || cmbInspection.SelectedItem == null)
            {
                MessageBox.Show("Сначала выберите трубопровод и инспекцию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataRowView inspRow = cmbInspection.SelectedItem as DataRowView;
                if (inspRow == null) return;
                int inspId = Convert.ToInt32(inspRow["id"]);

                DataTable defects = DatabaseHelper.ExecuteQuery(
                    "SELECT * FROM defects WHERE inspection_id=@id ORDER BY severity, erf DESC",
                    new MySqlParameter("@id", inspId));

                if (defects.Rows.Count == 0)
                {
                    MessageBox.Show("Нет дефектов для выбранной инспекции", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel files|*.xlsx";
                sfd.FileName = $"Дефекты_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ReportGenerator.ExportToExcel(defects, sfd.FileName);
                    MessageBox.Show($"Excel-файл сохранён:\n{sfd.FileName}", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AuditLogger.Log(Program.CurrentUser, "Экспорт Excel отчёта");
                }
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при экспорте в Excel", ex);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadPipelines();
                cmbPipeline.SelectedIndex = -1;
                cmbInspection.DataSource = null;
                cmbInspection.Enabled = false;
                btnGeneratePdf.Enabled = false;
                btnExportExcel.Enabled = false;
                lblStatus.Text = "Список обновлён. Выберите трубопровод";
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при обновлении", ex);
            }
        }

        private void ShowError(string message, Exception ex)
        {
            string errorMsg = $"{message}\n\n" +
                              $"Инструкция:\n" +
                              $"1. Проверьте подключение к базе данных\n" +
                              $"2. Убедитесь, что в таблицах pipelines и inspections есть данные\n" +
                              $"3. Попробуйте нажать 'Обновить список'\n\n" +
                              $"Техническая ошибка:\n{ex.Message}\n\n" +
                              $"Стек вызовов:\n{ex.StackTrace}";
            MessageBox.Show(errorMsg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            AuditLogger.LogError(Program.CurrentUser, message, ex);
        }
    }
}