namespace Pipe
{
    partial class DefectAnalysisForm
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

            this.filterPanel = new System.Windows.Forms.Panel();
            this.cmbPipeline = new System.Windows.Forms.ComboBox();
            this.lblPipeline = new System.Windows.Forms.Label();
            this.cmbInspection = new System.Windows.Forms.ComboBox();
            this.lblInspection = new System.Windows.Forms.Label();

            this.infoPanel = new System.Windows.Forms.Panel();
            this.lblPipeInfo = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();

            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabTable = new System.Windows.Forms.TabPage();
            this.dataGridViewDefects = new System.Windows.Forms.DataGridView();
            this.tabCanvas = new System.Windows.Forms.TabPage();
            this.panelCanvas = new System.Windows.Forms.Panel();

            this.buttonPanel = new System.Windows.Forms.Panel();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnFilterCritical = new System.Windows.Forms.Button();
            this.btnResetFilter = new System.Windows.Forms.Button();
            this.btnRecalc = new System.Windows.Forms.Button();
            this.btnResetView = new System.Windows.Forms.Button();
            this.btnToggleGrid = new System.Windows.Forms.Button();

            this.statusPanel = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();

            this.headerPanel.SuspendLayout();
            this.filterPanel.SuspendLayout();
            this.infoPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabTable.SuspendLayout();
            this.tabCanvas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDefects)).BeginInit();
            this.buttonPanel.SuspendLayout();
            this.statusPanel.SuspendLayout();
            this.SuspendLayout();

            // ========== HEADER PANEL ==========
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(0, 70, 128);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Height = 70;
            this.headerPanel.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);

            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(20, 10);
            this.titleLabel.Text = "Анализ дефектов";

            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.subtitleLabel.Location = new System.Drawing.Point(22, 40);
            this.subtitleLabel.Text = "Визуализация и анализ результатов внутритрубной диагностики";

            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.subtitleLabel);

            // ========== FILTER PANEL ==========
            this.filterPanel.BackColor = System.Drawing.Color.White;
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterPanel.Height = 60;
            this.filterPanel.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);

            this.lblPipeline.AutoSize = true;
            this.lblPipeline.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPipeline.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblPipeline.Location = new System.Drawing.Point(20, 18);
            this.lblPipeline.Text = "Трубопровод:";

            this.cmbPipeline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPipeline.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPipeline.Location = new System.Drawing.Point(130, 14);
            this.cmbPipeline.Size = new System.Drawing.Size(300, 25);
            this.cmbPipeline.SelectedIndexChanged += new System.EventHandler(this.cmbPipeline_SelectedIndexChanged);

            this.lblInspection.AutoSize = true;
            this.lblInspection.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInspection.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblInspection.Location = new System.Drawing.Point(480, 18);
            this.lblInspection.Text = "Инспекция:";

            this.cmbInspection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInspection.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbInspection.Location = new System.Drawing.Point(570, 14);
            this.cmbInspection.Size = new System.Drawing.Size(200, 25);
            this.cmbInspection.Enabled = false;
            this.cmbInspection.SelectedIndexChanged += new System.EventHandler(this.cmbInspection_SelectedIndexChanged);

            this.filterPanel.Controls.Add(this.lblPipeline);
            this.filterPanel.Controls.Add(this.cmbPipeline);
            this.filterPanel.Controls.Add(this.lblInspection);
            this.filterPanel.Controls.Add(this.cmbInspection);

            // ========== INFO PANEL ==========
            this.infoPanel.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoPanel.Height = 45;
            this.infoPanel.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);

            this.lblPipeInfo.AutoSize = true;
            this.lblPipeInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPipeInfo.ForeColor = System.Drawing.Color.FromArgb(0, 70, 128);
            this.lblPipeInfo.Location = new System.Drawing.Point(20, 14);
            this.lblPipeInfo.Text = "Выберите трубопровод";

            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProgress.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            this.lblProgress.Location = new System.Drawing.Point(500, 14);
            this.lblProgress.Text = "";

            this.infoPanel.Controls.Add(this.lblPipeInfo);
            this.infoPanel.Controls.Add(this.lblProgress);

            // ========== TAB CONTROL ==========
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.tabTable.Text = "Таблица дефектов";
            this.tabCanvas.Text = "Развёртка трубы";

            this.tabControl.Controls.Add(this.tabTable);
            this.tabControl.Controls.Add(this.tabCanvas);

            // ========== DATA GRID VIEW ==========
            this.dataGridViewDefects.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewDefects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewDefects.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewDefects.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewDefects.EnableHeadersVisualStyles = false;
            this.dataGridViewDefects.AllowUserToAddRows = false;
            this.dataGridViewDefects.AllowUserToDeleteRows = false;
            this.dataGridViewDefects.ReadOnly = true;
            this.dataGridViewDefects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDefects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewDefects.RowHeadersVisible = false;
            this.dataGridViewDefects.Dock = System.Windows.Forms.DockStyle.Fill;

            this.dataGridViewDefects.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dataGridViewDefects.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.dataGridViewDefects.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridViewDefects.ColumnHeadersHeight = 40;

            this.dataGridViewDefects.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dataGridViewDefects.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(32, 32, 32);
            this.dataGridViewDefects.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.dataGridViewDefects.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);

            this.dataGridViewDefects.RowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dataGridViewDefects.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.dataGridViewDefects.RowTemplate.Height = 35;

            this.tabTable.Controls.Add(this.dataGridViewDefects);

            // ========== CANVAS PANEL ==========
            this.panelCanvas.BackColor = System.Drawing.Color.White;
            this.panelCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCanvas.Controls.Add(this.panelCanvas);

            // ========== BUTTON PANEL ==========
            this.buttonPanel.BackColor = System.Drawing.Color.White;
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Height = 60;
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);

            // Кнопка Импорт CSV
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnImport.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnImport.Text = "Импорт CSV";
            this.btnImport.Size = new System.Drawing.Size(110, 38);
            this.btnImport.FlatAppearance.BorderSize = 1;
            this.btnImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);

            // Кнопка Только High / Critical
            this.btnFilterCritical.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterCritical.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnFilterCritical.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnFilterCritical.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnFilterCritical.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFilterCritical.Text = "Только High / Critical";
            this.btnFilterCritical.Size = new System.Drawing.Size(150, 38);
            this.btnFilterCritical.FlatAppearance.BorderSize = 1;
            this.btnFilterCritical.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilterCritical.Click += new System.EventHandler(this.btnFilterCritical_Click);

            // Кнопка Сброс фильтра
            this.btnResetFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetFilter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnResetFilter.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnResetFilter.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnResetFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnResetFilter.Text = "Сброс фильтра";
            this.btnResetFilter.Size = new System.Drawing.Size(120, 38);
            this.btnResetFilter.FlatAppearance.BorderSize = 1;
            this.btnResetFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetFilter.Click += new System.EventHandler(this.btnResetFilter_Click);

            // Кнопка Пересчитать ERF
            this.btnRecalc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecalc.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnRecalc.ForeColor = System.Drawing.Color.White;
            this.btnRecalc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRecalc.Text = "Пересчитать ERF";
            this.btnRecalc.Size = new System.Drawing.Size(130, 38);
            this.btnRecalc.FlatAppearance.BorderSize = 0;
            this.btnRecalc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecalc.Click += new System.EventHandler(this.btnRecalc_Click);

            // Кнопка Сброс вида
            this.btnResetView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetView.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnResetView.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnResetView.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnResetView.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnResetView.Text = "Сброс вида";
            this.btnResetView.Size = new System.Drawing.Size(100, 38);
            this.btnResetView.FlatAppearance.BorderSize = 1;
            this.btnResetView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetView.Click += new System.EventHandler(this.btnResetView_Click);

            // Кнопка Сетка: Вкл/Выкл
            this.btnToggleGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleGrid.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnToggleGrid.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnToggleGrid.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnToggleGrid.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnToggleGrid.Text = "Сетка: Вкл";
            this.btnToggleGrid.Size = new System.Drawing.Size(100, 38);
            this.btnToggleGrid.FlatAppearance.BorderSize = 1;
            this.btnToggleGrid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleGrid.Click += new System.EventHandler(this.btnToggleGrid_Click);

            // Размещение кнопок в FlowLayoutPanel
            var flowButtons = new System.Windows.Forms.FlowLayoutPanel();
            flowButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            flowButtons.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            flowButtons.Controls.Add(this.btnImport);
            flowButtons.Controls.Add(this.btnFilterCritical);
            flowButtons.Controls.Add(this.btnResetFilter);
            flowButtons.Controls.Add(this.btnRecalc);
            flowButtons.Controls.Add(this.btnResetView);
            flowButtons.Controls.Add(this.btnToggleGrid);

            this.buttonPanel.Controls.Add(flowButtons);

            // ========== STATUS PANEL ==========
            this.statusPanel.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.statusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusPanel.Height = 30;
            this.statusPanel.Padding = new System.Windows.Forms.Padding(20, 5, 20, 5);

            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            this.lblStatus.Text = "Импортируйте CSV файлы с данными дефектов | Цветовая индикация по категориям опасности";

            this.statusPanel.Controls.Add(this.lblStatus);

            // ========== MAIN FORM ==========
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.infoPanel);
            this.Controls.Add(this.filterPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.statusPanel);
            this.MinimumSize = new System.Drawing.Size(1100, 600);
            this.Text = "Анализ дефектов";
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.headerPanel.ResumeLayout(false);
            this.filterPanel.ResumeLayout(false);
            this.infoPanel.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabTable.ResumeLayout(false);
            this.tabCanvas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDefects)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.statusPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.ComboBox cmbPipeline;
        private System.Windows.Forms.Label lblPipeline;
        private System.Windows.Forms.ComboBox cmbInspection;
        private System.Windows.Forms.Label lblInspection;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.Label lblPipeInfo;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabTable;
        private System.Windows.Forms.DataGridView dataGridViewDefects;
        private System.Windows.Forms.TabPage tabCanvas;
        private System.Windows.Forms.Panel panelCanvas;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnFilterCritical;
        private System.Windows.Forms.Button btnResetFilter;
        private System.Windows.Forms.Button btnRecalc;
        private System.Windows.Forms.Button btnResetView;
        private System.Windows.Forms.Button btnToggleGrid;
        private System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Label lblStatus;
    }
}