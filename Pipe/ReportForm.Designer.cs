namespace Pipe
{
    partial class ReportForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();

            this.selectionPanel = new System.Windows.Forms.Panel();
            this.lblPipeline = new System.Windows.Forms.Label();
            this.cmbPipeline = new System.Windows.Forms.ComboBox();
            this.lblInspection = new System.Windows.Forms.Label();
            this.cmbInspection = new System.Windows.Forms.ComboBox();

            this.buttonPanel = new System.Windows.Forms.Panel();
            this.btnGeneratePdf = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();

            this.statusPanel = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();

            this.headerPanel.SuspendLayout();
            this.selectionPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.statusPanel.SuspendLayout();
            this.SuspendLayout();

            // ========== HEADER PANEL ==========
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(0, 70, 128);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Height = 80;
            this.headerPanel.Padding = new System.Windows.Forms.Padding(25, 15, 25, 15);

            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(25, 15);
            this.titleLabel.Text = "Генерация отчётов";

            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.subtitleLabel.Location = new System.Drawing.Point(27, 48);
            this.subtitleLabel.Text = "Формирование PDF и Excel отчётов по результатам диагностики";

            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.subtitleLabel);

            // ========== SELECTION PANEL ==========
            this.selectionPanel.BackColor = System.Drawing.Color.White;
            this.selectionPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectionPanel.Height = 130;
            this.selectionPanel.Padding = new System.Windows.Forms.Padding(25, 20, 25, 20);

            // Трубопровод
            this.lblPipeline.AutoSize = true;
            this.lblPipeline.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblPipeline.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblPipeline.Location = new System.Drawing.Point(25, 28);
            this.lblPipeline.Text = "Трубопровод:";

            this.cmbPipeline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPipeline.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbPipeline.Location = new System.Drawing.Point(150, 24);
            this.cmbPipeline.Size = new System.Drawing.Size(400, 28);
            this.cmbPipeline.SelectedIndexChanged += new System.EventHandler(this.cmbPipeline_SelectedIndexChanged);

            // Инспекция
            this.lblInspection.AutoSize = true;
            this.lblInspection.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblInspection.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblInspection.Location = new System.Drawing.Point(25, 75);
            this.lblInspection.Text = "Инспекция:";

            this.cmbInspection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInspection.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbInspection.Location = new System.Drawing.Point(150, 71);
            this.cmbInspection.Size = new System.Drawing.Size(400, 28);
            this.cmbInspection.Enabled = false;
            this.cmbInspection.SelectedIndexChanged += new System.EventHandler(this.cmbInspection_SelectedIndexChanged);

            this.selectionPanel.Controls.Add(this.lblPipeline);
            this.selectionPanel.Controls.Add(this.cmbPipeline);
            this.selectionPanel.Controls.Add(this.lblInspection);
            this.selectionPanel.Controls.Add(this.cmbInspection);

            // ========== BUTTON PANEL ==========
            this.buttonPanel.BackColor = System.Drawing.Color.White;
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(25, 30, 25, 30);

            // Кнопка PDF
            this.btnGeneratePdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeneratePdf.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnGeneratePdf.ForeColor = System.Drawing.Color.White;
            this.btnGeneratePdf.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnGeneratePdf.Text = "Сформировать PDF-отчёт (критические дефекты)";
            this.btnGeneratePdf.Size = new System.Drawing.Size(350, 55);
            this.btnGeneratePdf.FlatAppearance.BorderSize = 0;
            this.btnGeneratePdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGeneratePdf.Enabled = false;
            this.btnGeneratePdf.Click += new System.EventHandler(this.btnGeneratePdf_Click);

            // Кнопка Excel
            this.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnExportExcel.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnExportExcel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnExportExcel.Text = "Экспорт в Excel (все дефекты)";
            this.btnExportExcel.Size = new System.Drawing.Size(280, 55);
            this.btnExportExcel.FlatAppearance.BorderSize = 1;
            this.btnExportExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportExcel.Enabled = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

            // Кнопка Обновить
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnRefresh.Text = "Обновить список";
            this.btnRefresh.Size = new System.Drawing.Size(160, 55);
            this.btnRefresh.FlatAppearance.BorderSize = 1;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // Размещение кнопок
            var flowButtons = new System.Windows.Forms.FlowLayoutPanel();
            flowButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            flowButtons.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            flowButtons.Controls.Add(this.btnGeneratePdf);
            flowButtons.Controls.Add(this.btnExportExcel);
            flowButtons.Controls.Add(this.btnRefresh);

            this.buttonPanel.Controls.Add(flowButtons);

            // ========== STATUS PANEL ==========
            this.statusPanel.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.statusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusPanel.Height = 35;
            this.statusPanel.Padding = new System.Windows.Forms.Padding(25, 8, 25, 8);

            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            this.lblStatus.Location = new System.Drawing.Point(25, 10);
            this.lblStatus.Text = "Выберите трубопровод и инспекцию для формирования отчёта";

            this.statusPanel.Controls.Add(this.lblStatus);

            // ========== MAIN FORM ==========
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 400);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.selectionPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.statusPanel);
            this.MinimumSize = new System.Drawing.Size(650, 400);
            this.MaximumSize = new System.Drawing.Size(800, 500);
            this.Text = "Генерация отчётов";
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            this.headerPanel.ResumeLayout(false);
            this.selectionPanel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.statusPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Panel selectionPanel;
        private System.Windows.Forms.Label lblPipeline;
        private System.Windows.Forms.ComboBox cmbPipeline;
        private System.Windows.Forms.Label lblInspection;
        private System.Windows.Forms.ComboBox cmbInspection;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button btnGeneratePdf;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Label lblStatus;
    }
}