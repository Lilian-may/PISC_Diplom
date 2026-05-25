namespace Pipe
{
    partial class ImportInspectionsForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblInstruction = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Location = new System.Drawing.Point(12, 15);
            this.lblInstruction.Text = "Выберите файл Excel/CSV с колонками: pipeline_name, inspection_date, tool_type, speed_mps, coverage_percent, status";

            this.txtFilePath.Location = new System.Drawing.Point(12, 50);
            this.txtFilePath.Size = new System.Drawing.Size(400, 20);

            this.btnSelectFile.Location = new System.Drawing.Point(420, 48);
            this.btnSelectFile.Size = new System.Drawing.Size(100, 25);
            this.btnSelectFile.Text = "Обзор...";
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);

            this.btnImport.Location = new System.Drawing.Point(120, 90);
            this.btnImport.Size = new System.Drawing.Size(100, 35);
            this.btnImport.Text = "Импорт";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);

            this.btnCancel.Location = new System.Drawing.Point(240, 90);
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 140);
            this.lblStatus.Text = "Готов к импорту";

            this.ClientSize = new System.Drawing.Size(550, 180);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.lblInstruction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Импорт инспекций";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblStatus;
    }
}