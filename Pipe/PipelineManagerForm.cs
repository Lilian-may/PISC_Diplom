#nullable disable
using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Pipe
{
    public partial class PipelineManagerForm : Form
    {
        public PipelineManagerForm()
        {
            try
            {
                InitializeComponent();
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при загрузке формы управления трубопроводами.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void RefreshData()
        {
            try
            {
                var dt = DatabaseHelper.ExecuteQuery("SELECT id, name, start_km, end_km, diameter_mm, wall_thickness_mm, steel_grade, design_pressure_mpa, operating_pressure_mpa, region FROM pipelines");
                dataGridView.DataSource = dt;
                dataGridView.AutoResizeColumns();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new PipelineEditForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                        RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось открыть форму добавления трубопровода.\n\n" +
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
                    MessageBox.Show("Выберите трубопровод для редактирования.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int id = Convert.ToInt32(dataGridView.CurrentRow.Cells["id"].Value);
                using (var form = new PipelineEditForm(id))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                        RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось открыть форму редактирования трубопровода.\n\n" +
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
                    MessageBox.Show("Выберите трубопровод для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Удалить трубопровод? Все связанные данные будут удалены.", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView.CurrentRow.Cells["id"].Value);
                    DatabaseHelper.ExecuteNonQuery("DELETE FROM pipelines WHERE id=@id", new MySqlParameter("@id", id));
                    RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось удалить трубопровод.\n\n" +
                    "Возможно, существуют связанные инспекции или проблемы с базой данных.\n\n" +
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
                using (var form = new ImportPipelinesForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                        RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось открыть форму импорта трубопроводов.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}