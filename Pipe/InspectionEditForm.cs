#nullable disable
using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Pipe
{
    public partial class InspectionEditForm : Form
    {
        private int pipelineId;
        private int? editId = null;

        public InspectionEditForm(int id, bool isEdit = false)
        {
            try
            {
                InitializeComponent();
                if (isEdit)
                {
                    editId = id;
                    this.Text = "Редактирование инспекции";
                    LoadData(id);
                }
                else
                {
                    pipelineId = id;
                    this.Text = "Добавление инспекции";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка инициализации формы: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadData(int id)
        {
            try
            {
                var dt = DatabaseHelper.ExecuteQuery("SELECT * FROM inspections WHERE id=@id", new MySqlParameter("@id", id));
                if (dt.Rows.Count == 0) return;
                var row = dt.Rows[0];
                pipelineId = Convert.ToInt32(row["pipeline_id"]);
                dtpDate.Value = Convert.ToDateTime(row["inspection_date"]);
                cmbToolType.SelectedItem = row["tool_type"].ToString();
                txtSpeed.Text = row["speed_mps"].ToString();
                txtCoverage.Text = row["coverage_percent"].ToString();
                txtStatus.Text = row["status"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось загрузить данные инспекции для редактирования.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(txtSpeed.Text, out double speed) && !string.IsNullOrEmpty(txtSpeed.Text))
                {
                    MessageBox.Show("Скорость должна быть числом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtCoverage.Text, out int coverage)) coverage = 100;
                if (coverage < 0 || coverage > 100) coverage = 100;
                if (string.IsNullOrWhiteSpace(txtStatus.Text)) txtStatus.Text = "Выполнена";

                string sql;
                MySqlParameter[] pars;
                if (editId.HasValue)
                {
                    sql = @"UPDATE inspections SET inspection_date=@date, tool_type=@type, speed_mps=@speed, 
                            coverage_percent=@cov, status=@status WHERE id=@id";
                    pars = new MySqlParameter[]
                    {
                        new MySqlParameter("@id", editId.Value),
                        new MySqlParameter("@date", dtpDate.Value),
                        new MySqlParameter("@type", cmbToolType.SelectedItem.ToString()),
                        new MySqlParameter("@speed", string.IsNullOrEmpty(txtSpeed.Text) ? DBNull.Value : (object)speed),
                        new MySqlParameter("@cov", coverage),
                        new MySqlParameter("@status", txtStatus.Text)
                    };
                }
                else
                {
                    sql = @"INSERT INTO inspections (pipeline_id, inspection_date, tool_type, speed_mps, coverage_percent, status)
                            VALUES (@pid, @date, @type, @speed, @cov, @status)";
                    pars = new MySqlParameter[]
                    {
                        new MySqlParameter("@pid", pipelineId),
                        new MySqlParameter("@date", dtpDate.Value),
                        new MySqlParameter("@type", cmbToolType.SelectedItem.ToString()),
                        new MySqlParameter("@speed", string.IsNullOrEmpty(txtSpeed.Text) ? DBNull.Value : (object)speed),
                        new MySqlParameter("@cov", coverage),
                        new MySqlParameter("@status", txtStatus.Text)
                    };
                }
                DatabaseHelper.ExecuteNonQuery(sql, pars);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось сохранить данные инспекции.\n\n" +
                    "Проверьте правильность заполнения всех полей и подключение к базе данных.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка сохранения",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}