#nullable disable
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Pipe
{
    public partial class InspectionManagerForm : Form
    {
        public InspectionManagerForm()
        {
            try
            {
                InitializeComponent();
                ApplyModernTheme();
                LoadPipelines();
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при загрузке формы управления инспекциями", ex);
            }
        }

        private void ApplyModernTheme()
        {
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
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

                if (dt.Rows.Count > 0)
                {
                    lblPipelineCount.Text = $"Всего трубопроводов: {dt.Rows.Count}";
                }
                else
                {
                    lblPipelineCount.Text = "Нет трубопроводов";
                }
            }
            catch (Exception ex)
            {
                ShowError("Не удалось загрузить список трубопроводов", ex);
            }
        }

        public void RefreshData()
        {
            LoadInspections();
        }

        private void LoadInspections()
        {
            try
            {
                if (cmbPipeline.SelectedItem == null)
                {
                    ClearInspectionDisplay();
                    return;
                }

                DataRowView selectedRow = cmbPipeline.SelectedItem as DataRowView;
                if (selectedRow == null)
                {
                    ClearInspectionDisplay();
                    return;
                }

                int pid = Convert.ToInt32(selectedRow["id"]);
                string pipelineName = selectedRow["name"].ToString();

                var dt = DatabaseHelper.ExecuteQuery(
                    "SELECT id, inspection_date, tool_type, speed_mps, coverage_percent, status FROM inspections WHERE pipeline_id=@pid ORDER BY inspection_date DESC",
                    new MySqlParameter("@pid", pid));

                dataGridView.DataSource = dt;
                dataGridView.AutoResizeColumns();

                lblCurrentPipeline.Text = pipelineName;
                lblInspectionCount.Text = dt.Rows.Count == 0
                    ? "Нет инспекций"
                    : $"Найдено инспекций: {dt.Rows.Count}";

                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                ShowError("Не удалось загрузить список инспекций", ex);
            }
        }

        private void ClearInspectionDisplay()
        {
            dataGridView.DataSource = null;
            lblCurrentPipeline.Text = "Не выбран";
            lblInspectionCount.Text = "Выберите трубопровод";
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void cmbPipeline_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadInspections();
            }
            catch (Exception ex)
            {
                ShowError("Ошибка при выборе трубопровода", ex);
            }
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = dataGridView.SelectedRows.Count > 0;
            btnDelete.Enabled = dataGridView.SelectedRows.Count > 0;
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditSelectedInspection();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPipeline.SelectedItem == null)
                {
                    MessageBox.Show("Сначала выберите трубопровод.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRowView selectedRow = cmbPipeline.SelectedItem as DataRowView;
                if (selectedRow == null) return;
                int pid = Convert.ToInt32(selectedRow["id"]);

                using (var form = new InspectionEditForm(pid, false))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadInspections();
                        AuditLogger.Log(Program.CurrentUser, "Добавление инспекции", "inspections");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Не удалось открыть форму добавления инспекции", ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditSelectedInspection();
        }

        private void EditSelectedInspection()
        {
            try
            {
                if (dataGridView.CurrentRow == null)
                {
                    MessageBox.Show("Выберите инспекцию для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int id = Convert.ToInt32(dataGridView.CurrentRow.Cells["id"].Value);

                using (var form = new InspectionEditForm(id, true))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadInspections();
                        AuditLogger.Log(Program.CurrentUser, "Редактирование инспекции", "inspections", id);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Не удалось открыть форму редактирования инспекции", ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.CurrentRow == null)
                {
                    MessageBox.Show("Выберите инспекцию для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Удалить инспекцию? Все дефекты будут удалены без возможности восстановления.",
                    "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView.CurrentRow.Cells["id"].Value);
                    DatabaseHelper.ExecuteNonQuery("DELETE FROM inspections WHERE id=@id", new MySqlParameter("@id", id));
                    AuditLogger.Log(Program.CurrentUser, "Удаление инспекции", "inspections", id);
                    LoadInspections();
                    MessageBox.Show("Инспекция успешно удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowError("Не удалось удалить инспекцию", ex);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new ImportInspectionsForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadInspections();
                        AuditLogger.Log(Program.CurrentUser, "Импорт инспекций из Excel");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Не удалось открыть форму импорта инспекций", ex);
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