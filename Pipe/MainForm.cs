#nullable disable
using System;
using System.Windows.Forms;

namespace Pipe
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            try
            {
                InitializeComponent();
                UpdateStatusBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при загрузке главного окна.\n\n" +
                    "Попробуйте перезапустить программу.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Критическая ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void UpdateStatusBar()
        {
            try
            {
                lblUser.Text = $"Пользователь: {Program.CurrentUser}";
                lblDbStatus.Text = "Подключение к БД: OK";
            }
            catch (Exception ex)
            {
                lblUser.Text = "Пользователь: ошибка";
                lblDbStatus.Text = "Подключение: ошибка";
                System.Diagnostics.Debug.WriteLine($"StatusBar error: {ex.Message}");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при завершении программы.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void pipelinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new PipelineManagerForm();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось открыть форму управления трубопроводами.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void inspectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new InspectionManagerForm();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось открыть форму управления инспекциями.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void defectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new DefectAnalysisForm();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось открыть форму анализа дефектов.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new ReportForm();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось открыть форму генерации отчётов.\n\n" +
                    $"Техническая ошибка: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка при отображении информации: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}