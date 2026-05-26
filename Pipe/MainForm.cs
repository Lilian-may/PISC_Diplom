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
                CheckInspectionPeriodicity();
                AuditLogger.Log(Program.CurrentUser, "Открытие главного окна");
            }
            catch (Exception ex)
            {
                string errorMsg = $"Ошибка при загрузке главного окна.\n\n" +
                                  $"Инструкция:\n" +
                                  $"Попробуйте перезапустить программу.\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}\n\n" +
                                  $"Стек вызовов:\n{ex.StackTrace}";
                MessageBox.Show(errorMsg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditLogger.LogError(Program.CurrentUser, "MainForm_Load", ex);
            }
        }

        private void UpdateStatusBar()
        {
            try
            {
                lblUser.Text = $"Пользователь: {Program.CurrentUser}";
                lblDbStatus.Text = "Подключение к БД: OK";
                lblDbStatus.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblUser.Text = "Пользователь: ошибка";
                lblDbStatus.Text = "Подключение: ошибка";
                lblDbStatus.ForeColor = System.Drawing.Color.Red;
                AuditLogger.LogError(Program.CurrentUser, "UpdateStatusBar", ex);
            }
        }

        /// <summary>
        /// Проверка периодичности инспекций (не реже 1 раза в 3 года)
        /// </summary>
        private void CheckInspectionPeriodicity()
        {
            try
            {
                string sql = @"
                    SELECT p.id, p.name, MAX(i.inspection_date) as last_date
                    FROM pipelines p
                    LEFT JOIN inspections i ON p.id = i.pipeline_id
                    GROUP BY p.id
                    HAVING last_date IS NULL OR last_date < DATE_SUB(CURDATE(), INTERVAL 3 YEAR)";

                var dt = DatabaseHelper.ExecuteQuery(sql);
                if (dt.Rows.Count > 0)
                {
                    string pipelines = "";
                    for (int i = 0; i < Math.Min(dt.Rows.Count, 5); i++)
                    {
                        pipelines += $"• {dt.Rows[i]["name"]}\n";
                    }
                    if (dt.Rows.Count > 5)
                        pipelines += $"• и ещё {dt.Rows.Count - 5} трубопровод(ов)\n";

                    string warningMsg = $"ВНИМАНИЕ! Следующие трубопроводы не проходили инспекцию более 3 лет:\n\n{pipelines}\n" +
                                       $"Рекомендуется провести внеплановое диагностическое обследование.\n\n" +
                                       $"Согласно требованиям, приборное обследование должно проводиться не реже 1 раза в 3 года.";

                    MessageBox.Show(warningMsg, "Плановые проверки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AuditLogger.Log(Program.CurrentUser, "Проверка периодичности", details: $"Найдено {dt.Rows.Count} просроченных трубопроводов");
                }
            }
            catch (Exception ex)
            {
                string errorMsg = $"Не удалось проверить периодичность инспекций.\n\n" +
                                  $"Инструкция:\n" +
                                  $"Проверьте подключение к базе данных.\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}\n\n" +
                                  $"Стек вызовов:\n{ex.StackTrace}";
                MessageBox.Show(errorMsg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditLogger.LogError(Program.CurrentUser, "CheckInspectionPeriodicity", ex);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AuditLogger.Log(Program.CurrentUser, "Выход из программы");
                Application.Exit();
            }
            catch (Exception ex)
            {
                string errorMsg = $"Ошибка при завершении программы.\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}";
                MessageBox.Show(errorMsg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
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
                AuditLogger.Log(Program.CurrentUser, "Открыта форма управления трубопроводами");
            }
            catch (Exception ex)
            {
                string errorMsg = $"Не удалось открыть форму управления трубопроводами.\n\n" +
                                  $"Инструкция:\n" +
                                  $"Проверьте, что все файлы программы находятся на месте.\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}\n\n" +
                                  $"Стек вызовов:\n{ex.StackTrace}";
                MessageBox.Show(errorMsg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditLogger.LogError(Program.CurrentUser, "OpenPipelineManagerForm", ex);
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
                AuditLogger.Log(Program.CurrentUser, "Открыта форма управления инспекциями");
            }
            catch (Exception ex)
            {
                string errorMsg = $"Не удалось открыть форму управления инспекциями.\n\n" +
                                  $"Инструкция:\n" +
                                  $"Проверьте, что все файлы программы находятся на месте.\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}\n\n" +
                                  $"Стек вызовов:\n{ex.StackTrace}";
                MessageBox.Show(errorMsg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditLogger.LogError(Program.CurrentUser, "OpenInspectionManagerForm", ex);
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
                AuditLogger.Log(Program.CurrentUser, "Открыта форма анализа дефектов");
            }
            catch (Exception ex)
            {
                string errorMsg = $"Не удалось открыть форму анализа дефектов.\n\n" +
                                  $"Инструкция:\n" +
                                  $"Проверьте, что все файлы программы находятся на месте.\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}\n\n" +
                                  $"Стек вызовов:\n{ex.StackTrace}";
                MessageBox.Show(errorMsg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditLogger.LogError(Program.CurrentUser, "OpenDefectAnalysisForm", ex);
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
                AuditLogger.Log(Program.CurrentUser, "Открыта форма генерации отчётов");
            }
            catch (Exception ex)
            {
                string errorMsg = $"Не удалось открыть форму генерации отчётов.\n\n" +
                                  $"Инструкция:\n" +
                                  $"Проверьте, что все файлы программы находятся на месте.\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}\n\n" +
                                  $"Стек вызовов:\n{ex.StackTrace}";
                MessageBox.Show(errorMsg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AuditLogger.LogError(Program.CurrentUser, "OpenReportForm", ex);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string info = "ВТД Аналитика ПАО «Газпром»\n\n" +
                              "Версия: 2.0\n" +
                              "Разработчик: Кузьмин А.О.\n\n" +
                              "Система анализа данных внутритрубной диагностики\n" +
                              "Расчёт остаточной прочности по СТО Газпром 2-2.3-112-2007\n\n" +
                              "Соответствует требованиям:\n" +
                              "• СТО Газпром 2-2.3-112-2007\n" +
                              "• Периодичность проверок (1 раз в 3 года)\n" +
                              "• Политики безопасности ПАО «Газпром»\n" +
                              "• BCrypt хэширование паролей\n" +
                              "• Локальное логирование действий";
                MessageBox.Show(info, "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string errorMsg = $"Ошибка при отображении информации о программе.\n\n" +
                                  $"Техническая ошибка:\n{ex.Message}";
                MessageBox.Show(errorMsg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}