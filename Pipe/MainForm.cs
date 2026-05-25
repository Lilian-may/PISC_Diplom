#nullable disable
using System;
using System.Windows.Forms;

namespace Pipe
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            lblUser.Text = $"Пользователь: {Program.CurrentUser}";
            lblDbStatus.Text = "Подключение к БД: OK";
        }

                private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

                private void pipelinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new PipelineManagerForm();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

                private void inspectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new InspectionManagerForm();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

                private void defectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new DefectAnalysisForm();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

                private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ReportForm();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

                private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "ВТД Аналитика ПАО «Газпром»\n\n" +
                "Версия: 1.0\n" +
                "Разработчик: Кузьмин А.О.\n\n" +
                "Система анализа данных внутритрубной диагностики\n" +
                "Расчёт остаточной прочности по СТО Газпром 2-2.3-112-2007",
                "О программе",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}