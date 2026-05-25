#nullable disable
using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Pipe
{
    public partial class InspectionManagerForm : Form
    {
        public InspectionManagerForm()
        {
            try
            {
                InitializeComponent();
                LoadPipelines();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при загрузке формы управления инспекциями.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadPipelines()
        {
            try
            {
                var dt = DatabaseHelper.ExecuteQuery("SELECT id, name FROM pipelines");
                cmbPipeline.DataSource = dt;
                cmbPipeline.DisplayMember = "name";
                cmbPipeline.ValueMember = "id";
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

        public void RefreshData()
        {
            LoadInspections();
        }

        private void LoadInspections()
        {
            try
            {
                if (cmbPipeline.SelectedValue == null) return;
                int pid = (int)cmbPipeline.SelectedValue;
                var dt = DatabaseHelper.ExecuteQuery("SELECT id, inspection_date, tool_type, speed_mps, coverage_percent, status FROM inspections WHERE pipeline_id=@pid", new MySqlParameter("@pid", pid));
                dataGridView.DataSource = dt;
                dataGridView.AutoResizeColumns();
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

        private void cmbPipeline_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadInspections();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка при смене трубопровода: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPipeline.SelectedValue == null)
                {
                    MessageBox.Show("Сначала выберите трубопровод.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int pid = (int)cmbPipeline.SelectedValue;
                using (var form = new InspectionEditForm(pid, false))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                        LoadInspections();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось открыть форму добавления инспекции.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.CurrentRow == null)
                {
                    MessageBox.Show("Выберите инспекцию для редактирования.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int id = (int)dataGridView.CurrentRow.Cells["id"].Value;
                using (var form = new InspectionEditForm(id, true))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                        LoadInspections();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось открыть форму редактирования инспекции.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.CurrentRow == null)
                {
                    MessageBox.Show("Выберите инспекцию для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Удалить инспекцию? Все дефекты будут удалены.", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id = (int)dataGridView.CurrentRow.Cells["id"].Value;
                    DatabaseHelper.ExecuteNonQuery("DELETE FROM inspections WHERE id=@id", new MySqlParameter("@id", id));
                    LoadInspections();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось удалить инспекцию.\n\n" +
                    "Возможно, существуют связанные дефекты или проблемы с базой данных.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка удаления",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new ImportInspectionsForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                        LoadInspections();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось открыть форму импорта инспекций.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}