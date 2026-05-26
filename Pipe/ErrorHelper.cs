using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pipe
{
    public static class ErrorHelper
    {
        public static void ShowErrorWithDetails(string userMessage, string title, Exception ex)
        {
            try
            {
                var result = MessageBox.Show(userMessage + "\n\nНажмите 'Да' чтобы показать подробности.", title, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    string details = ex.ToString();
                    using (var detailsForm = new Form())
                    {
                        detailsForm.Text = "Подробности ошибки";
                        detailsForm.ClientSize = new Size(800, 400);
                        detailsForm.StartPosition = FormStartPosition.CenterParent;

                        var textBox = new TextBox();
                        textBox.Multiline = true;
                        textBox.ReadOnly = true;
                        textBox.ScrollBars = ScrollBars.Both;
                        textBox.Dock = DockStyle.Fill;
                        textBox.Font = new Font("Consolas", 9);
                        textBox.Text = details;

                        detailsForm.Controls.Add(textBox);
                        detailsForm.ShowDialog();
                    }
                }
            }
            catch
            {
                try
                {
                    MessageBox.Show(userMessage + "\n\nТехническая ошибка: " + ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    // last resort: ignore
                }
            }
        }
    }
}
