#nullable disable
using OfficeOpenXml;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Pipe
{
    static class Program
    {
        public static string CurrentUser { get; set; }

        [STAThread]
        static void Main()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var login = new LoginForm())
            {
                if (login.ShowDialog() == DialogResult.OK)
                    Application.Run(new MainForm());
            }
        }
    }
}