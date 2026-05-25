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
            InitializeComponent();
            RefreshData();
        }

        public void RefreshData()
        {
            var dt = DatabaseHelper.ExecuteQuery("SELECT id, name, start_km, end_km, diameter_mm, wall_thickness_mm, steel_grade, design_pressure_mpa, operating_pressure_mpa, region FROM pipelines");
            dataGridView.DataSource = dt;
            dataGridView.AutoResizeColumns();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new PipelineEditForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    RefreshData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null) return;
            int id = Convert.ToInt32(dataGridView.CurrentRow.Cells["id"].Value);
            using (var form = new PipelineEditForm(id))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    RefreshData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null) return;
            if (MessageBox.Show("Удалить трубопровод? Все связанные данные будут удалены.", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dataGridView.CurrentRow.Cells["id"].Value);
                DatabaseHelper.ExecuteNonQuery("DELETE FROM pipelines WHERE id=@id", new MySqlParameter("@id", id));
                RefreshData();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (var form = new ImportPipelinesForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    RefreshData();
            }
        }
    }
}