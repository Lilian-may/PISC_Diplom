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
            InitializeComponent();
            LoadPipelines();
        }

        private void LoadPipelines()
        {
            var dt = DatabaseHelper.ExecuteQuery("SELECT id, name FROM pipelines");
            cmbPipeline.DataSource = dt;
            cmbPipeline.DisplayMember = "name";
            cmbPipeline.ValueMember = "id";
        }

        public void RefreshData()
        {
            LoadInspections();
        }

        private void LoadInspections()
        {
            if (cmbPipeline.SelectedValue == null) return;
            int pid = (int)cmbPipeline.SelectedValue;
            var dt = DatabaseHelper.ExecuteQuery("SELECT id, inspection_date, tool_type, speed_mps, coverage_percent, status FROM inspections WHERE pipeline_id=@pid", new MySqlParameter("@pid", pid));
            dataGridView.DataSource = dt;
            dataGridView.AutoResizeColumns();
        }

        private void cmbPipeline_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInspections();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbPipeline.SelectedValue == null) return;
            int pid = (int)cmbPipeline.SelectedValue;
            using (var form = new InspectionEditForm(pid, false))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadInspections();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null) return;
            int id = (int)dataGridView.CurrentRow.Cells["id"].Value;
            using (var form = new InspectionEditForm(id, true))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadInspections();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null) return;
            if (MessageBox.Show("Удалить инспекцию? Все дефекты будут удалены.", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id = (int)dataGridView.CurrentRow.Cells["id"].Value;
                DatabaseHelper.ExecuteNonQuery("DELETE FROM inspections WHERE id=@id", new MySqlParameter("@id", id));
                LoadInspections();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (var form = new ImportInspectionsForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadInspections();
            }
        }
    }
}