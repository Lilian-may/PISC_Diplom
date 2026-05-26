#nullable disable
using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Pipe
{
    public partial class PipelineEditForm : Form
    {
        private int? editId = null;

        public PipelineEditForm()
        {
            try
            {
                InitializeComponent();
                this.Text = "Добавление трубопровода";
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

        public PipelineEditForm(int id)
        {
            try
            {
                InitializeComponent();
                editId = id;
                this.Text = "Редактирование трубопровода";
                LoadData(id);
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

        private void LoadData(int id)
        {
            try
            {
                var dt = DatabaseHelper.ExecuteQuery("SELECT * FROM pipelines WHERE id=@id", new MySqlParameter("@id", id));
                if (dt.Rows.Count == 0) return;
                var row = dt.Rows[0];
                txtName.Text = row["name"].ToString();
                txtStartKm.Text = row["start_km"].ToString();
                txtEndKm.Text = row["end_km"].ToString();
                txtDiameter.Text = row["diameter_mm"].ToString();
                txtWallThickness.Text = row["wall_thickness_mm"].ToString();
                txtSteelGrade.Text = row["steel_grade"].ToString();
                txtYieldStrength.Text = row["yield_strength_mpa"].ToString();
                txtDesignPressure.Text = row["design_pressure_mpa"].ToString();
                txtOperatingPressure.Text = row["operating_pressure_mpa"].ToString();
                txtRegion.Text = row["region"].ToString();
                dtpCommissioning.Value = Convert.ToDateTime(row["commissioning_date"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось загрузить данные трубопровода для редактирования.\n\n" +
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
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Введите наименование трубопровода", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!double.TryParse(txtDiameter.Text, out double diam) || diam <= 0)
                {
                    MessageBox.Show("Диаметр должен быть положительным числом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!double.TryParse(txtWallThickness.Text, out double wall) || wall <= 0)
                {
                    MessageBox.Show("Толщина стенки должна быть положительным числом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!double.TryParse(txtYieldStrength.Text, out double yield) || yield <= 0)
                {
                    MessageBox.Show("Предел текучести должен быть положительным числом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!double.TryParse(txtDesignPressure.Text, out double desPress) || desPress <= 0)
                {
                    MessageBox.Show("Проектное давление должно быть положительным числом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!double.TryParse(txtOperatingPressure.Text, out double opPress) || opPress <= 0)
                {
                    MessageBox.Show("Рабочее давление должно быть положительным числом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!double.TryParse(txtStartKm.Text, out double startKm)) startKm = 0;
                if (!double.TryParse(txtEndKm.Text, out double endKm) || endKm <= startKm)
                {
                    MessageBox.Show("Конечный километраж должен быть больше начального", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sql;
                MySqlParameter[] pars;
                if (editId.HasValue)
                {
                    sql = @"UPDATE pipelines SET name=@name, start_km=@start, end_km=@end, diameter_mm=@diam, 
                            wall_thickness_mm=@wall, steel_grade=@steel, yield_strength_mpa=@yield,
                            design_pressure_mpa=@des, operating_pressure_mpa=@op, region=@region, 
                            commissioning_date=@date WHERE id=@id";
                    pars = new MySqlParameter[]
                    {
                        new MySqlParameter("@id", editId.Value),
                        new MySqlParameter("@name", txtName.Text),
                        new MySqlParameter("@start", startKm),
                        new MySqlParameter("@end", endKm),
                        new MySqlParameter("@diam", diam),
                        new MySqlParameter("@wall", wall),
                        new MySqlParameter("@steel", txtSteelGrade.Text),
                        new MySqlParameter("@yield", yield),
                        new MySqlParameter("@des", desPress),
                        new MySqlParameter("@op", opPress),
                        new MySqlParameter("@region", txtRegion.Text),
                        new MySqlParameter("@date", dtpCommissioning.Value)
                    };
                }
                else
                {
                    sql = @"INSERT INTO pipelines (name, start_km, end_km, diameter_mm, wall_thickness_mm, steel_grade, 
                            yield_strength_mpa, design_pressure_mpa, operating_pressure_mpa, region, commissioning_date)
                            VALUES (@name, @start, @end, @diam, @wall, @steel, @yield, @des, @op, @region, @date)";
                    pars = new MySqlParameter[]
                    {
                        new MySqlParameter("@name", txtName.Text),
                        new MySqlParameter("@start", startKm),
                        new MySqlParameter("@end", endKm),
                        new MySqlParameter("@diam", diam),
                        new MySqlParameter("@wall", wall),
                        new MySqlParameter("@steel", txtSteelGrade.Text),
                        new MySqlParameter("@yield", yield),
                        new MySqlParameter("@des", desPress),
                        new MySqlParameter("@op", opPress),
                        new MySqlParameter("@region", txtRegion.Text),
                        new MySqlParameter("@date", dtpCommissioning.Value)
                    };
                }
                DatabaseHelper.ExecuteNonQuery(sql, pars);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось сохранить данные трубопровода.\n\n" +
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