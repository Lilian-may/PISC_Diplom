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
            this.headerPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();

            this.contentPanel = new System.Windows.Forms.Panel();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();

            this.buttonPanel = new System.Windows.Forms.Panel();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();

            // ========== HEADER PANEL ==========
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(0, 70, 128);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Height = 70;
            this.headerPanel.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);

            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(20, 15);
            this.titleLabel.Text = "Импорт инспекций";

            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.subtitleLabel.Location = new System.Drawing.Point(22, 45);
            this.subtitleLabel.Text = "Загрузка данных из Excel или CSV файла";

            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.subtitleLabel);

            // ========== CONTENT PANEL ==========
            this.contentPanel.BackColor = System.Drawing.Color.White;
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Padding = new System.Windows.Forms.Padding(25, 25, 25, 25);

            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInstruction.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblInstruction.Location = new System.Drawing.Point(25, 25);
            this.lblInstruction.Text = "Выберите файл Excel (xlsx, xls) или CSV с колонками:";

            var instructionDetails = new System.Windows.Forms.Label();
            instructionDetails.AutoSize = true;
            instructionDetails.Font = new System.Drawing.Font("Segoe UI", 8F);
            instructionDetails.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            instructionDetails.Location = new System.Drawing.Point(25, 50);
            instructionDetails.Text = "pipeline_name, inspection_date, tool_type, speed_mps, coverage_percent, status";

            this.txtFilePath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFilePath.Location = new System.Drawing.Point(25, 90);
            this.txtFilePath.Size = new System.Drawing.Size(450, 27);
            this.txtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilePath.ReadOnly = true;

            this.btnSelectFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnSelectFile.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnSelectFile.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnSelectFile.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSelectFile.Text = "Обзор...";
            this.btnSelectFile.Size = new System.Drawing.Size(100, 27);
            this.btnSelectFile.FlatAppearance.BorderSize = 1;
            this.btnSelectFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectFile.Location = new System.Drawing.Point(485, 88);
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);

            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            this.lblStatus.Location = new System.Drawing.Point(25, 140);
            this.lblStatus.Text = "Готов к импорту";

            this.contentPanel.Controls.Add(this.lblInstruction);
            this.contentPanel.Controls.Add(instructionDetails);
            this.contentPanel.Controls.Add(this.txtFilePath);
            this.contentPanel.Controls.Add(this.btnSelectFile);
            this.contentPanel.Controls.Add(this.lblStatus);

            // ========== BUTTON PANEL ==========
            this.buttonPanel.BackColor = System.Drawing.Color.White;
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Height = 65;
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(25, 12, 25, 12);

            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnImport.Text = "Импорт";
            this.btnImport.Size = new System.Drawing.Size(120, 40);
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);

            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.FlatAppearance.BorderSize = 1;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            var flowButtons = new System.Windows.Forms.FlowLayoutPanel();
            flowButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            flowButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            flowButtons.Controls.Add(this.btnImport);
            flowButtons.Controls.Add(this.btnCancel);

            this.buttonPanel.Controls.Add(flowButtons);

            // ========== MAIN FORM ==========
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 300);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.buttonPanel);
            this.MinimumSize = new System.Drawing.Size(650, 300);
            this.MaximumSize = new System.Drawing.Size(800, 400);
            this.Text = "Импорт инспекций";
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            this.headerPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnCancel;
    }
}