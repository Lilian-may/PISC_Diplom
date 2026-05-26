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
                LoadPipelines();
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorWithDetails(
                    "Ошибка инициализации формы отчётов.\n\nПроверьте настройки и повторите попытку.",
                    "Ошибка",
                    ex);
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
                if (dt.Rows.Count == 0)
                {
                    cmbPipeline.Enabled = false;
                    cmbInspection.Enabled = false;
                    btnGeneratePdf.Enabled = false;
                    btnExportExcel.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorWithDetails(
                    "Не удалось загрузить список трубопроводов.\n\nПроверьте подключение к базе данных и повторите попытку.",
                    "Ошибка загрузки",
                    ex);
            }
        }

        private void cmbPipeline_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPipeline.SelectedItem == null) return;
                DataRowView selectedRow = cmbPipeline.SelectedItem as DataRowView;
                if (selectedRow == null) return;
                int pid = Convert.ToInt32(selectedRow["id"]);
                LoadInspections(pid);
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorWithDetails(
                    "Ошибка при выборе трубопровода.\n\nПопробуйте выбрать снова.",
                    "Ошибка",
                    ex);
            }
        }

        private void LoadInspections(int pipelineId)
        {
            try
            {
                var dt = DatabaseHelper.ExecuteQuery(
                    "SELECT id, inspection_date, tool_type FROM inspections WHERE pipeline_id=@pid ORDER BY inspection_date DESC",
                    new MySqlParameter("@pid", pipelineId));
                cmbInspection.DataSource = dt;
                cmbInspection.DisplayMember = "inspection_date";
                cmbInspection.ValueMember = "id";
                if (dt.Rows.Count == 0)
                {
                    cmbInspection.Enabled = false;
                    btnGeneratePdf.Enabled = false;
                    btnExportExcel.Enabled = false;
                    lblStatus.Text = "Нет инспекций для выбранного трубопровода";
                }
                else
                {
                    cmbInspection.Enabled = true;
                    btnGeneratePdf.Enabled = true;
                    btnExportExcel.Enabled = true;
                    lblStatus.Text = $"Найдено инспекций: {dt.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorWithDetails(
                    "Не удалось загрузить список инспекций.\n\nПроверьте подключение к базе данных и повторите попытку.",
                    "Ошибка загрузки",
                    ex);
                cmbInspection.Enabled = false;
            }
        }

        private void btnGeneratePdf_Click(object sender, EventArgs e)
        {
            if (cmbPipeline.SelectedItem == null || cmbInspection.SelectedItem == null)
            {
                MessageBox.Show("Выберите трубопровод и инспекцию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                DataTable defects = DatabaseHelper.ExecuteQuery(
                    "SELECT * FROM defects WHERE inspection_id=@id AND severity IN ('High','Critical') ORDER BY erf DESC",
                    new MySqlParameter("@id", inspId));
                DataTable pipeData = DatabaseHelper.ExecuteQuery("SELECT * FROM pipelines WHERE id=@id", new MySqlParameter("@id", pipeId));
                DataTable inspData = DatabaseHelper.ExecuteQuery("SELECT * FROM inspections WHERE id=@id", new MySqlParameter("@id", inspId));
                if (pipeData.Rows.Count == 0 || inspData.Rows.Count == 0)
                {
                    MessageBox.Show("Данные не найдены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (defects.Rows.Count == 0)
                {
                    MessageBox.Show("Нет критических дефектов (High/Critical) для выбранной инспекции",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorWithDetails(
                    "Не удалось сформировать PDF-отчёт.\n\nПроверьте, что выбраны трубопровод и инспекция, и есть критические дефекты.",
                    "Ошибка генерации PDF",
                    ex);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (cmbPipeline.SelectedItem == null || cmbInspection.SelectedItem == null)
            {
                MessageBox.Show("Выберите трубопровод и инспекцию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorWithDetails(
                    "Не удалось экспортировать данные в Excel.\n\nПроверьте, что выбраны трубопровод и инспекция.",
                    "Ошибка экспорта в Excel",
                    ex);
            }
        }
    }
}