namespace Pipe
{
    partial class InspectionManagerForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Основные компоненты
            this.headerPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();

            this.selectionPanel = new System.Windows.Forms.Panel();
            this.cmbPipeline = new System.Windows.Forms.ComboBox();
            this.lblPipeline = new System.Windows.Forms.Label();
            this.lblPipelineCount = new System.Windows.Forms.Label();

            this.infoPanel = new System.Windows.Forms.Panel();
            this.lblCurrentPipelineTitle = new System.Windows.Forms.Label();
            this.lblCurrentPipeline = new System.Windows.Forms.Label();
            this.lblInspectionCount = new System.Windows.Forms.Label();

            this.dataGridView = new System.Windows.Forms.DataGridView();

            this.buttonPanel = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();

            this.statusPanel = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();

            // Панели
            this.headerPanel.SuspendLayout();
            this.selectionPanel.SuspendLayout();
            this.infoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
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
            this.titleLabel.Text = "Управление инспекциями";

            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.subtitleLabel.Location = new System.Drawing.Point(22, 40);
            this.subtitleLabel.Text = "Просмотр и управление диагностическими обследованиями трубопроводов";

            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.subtitleLabel);

            // ========== SELECTION PANEL ==========
            this.selectionPanel.BackColor = System.Drawing.Color.White;
            this.selectionPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectionPanel.Height = 60;
            this.selectionPanel.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);

            this.lblPipeline.AutoSize = true;
            this.lblPipeline.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPipeline.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblPipeline.Location = new System.Drawing.Point(20, 18);
            this.lblPipeline.Text = "Трубопровод:";

            this.cmbPipeline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPipeline.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPipeline.Location = new System.Drawing.Point(130, 14);
            this.cmbPipeline.Size = new System.Drawing.Size(350, 25);
            this.cmbPipeline.SelectedIndexChanged += new System.EventHandler(this.cmbPipeline_SelectedIndexChanged);

            this.lblPipelineCount.AutoSize = true;
            this.lblPipelineCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPipelineCount.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            this.lblPipelineCount.Location = new System.Drawing.Point(500, 18);
            this.lblPipelineCount.Text = "Всего трубопроводов: 0";

            this.selectionPanel.Controls.Add(this.lblPipeline);
            this.selectionPanel.Controls.Add(this.cmbPipeline);
            this.selectionPanel.Controls.Add(this.lblPipelineCount);

            // ========== INFO PANEL ==========
            this.infoPanel.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoPanel.Height = 45;
            this.infoPanel.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);

            this.lblCurrentPipelineTitle.AutoSize = true;
            this.lblCurrentPipelineTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCurrentPipelineTitle.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblCurrentPipelineTitle.Location = new System.Drawing.Point(20, 14);
            this.lblCurrentPipelineTitle.Text = "Текущий трубопровод:";

            this.lblCurrentPipeline.AutoSize = true;
            this.lblCurrentPipeline.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCurrentPipeline.ForeColor = System.Drawing.Color.FromArgb(0, 70, 128);
            this.lblCurrentPipeline.Location = new System.Drawing.Point(160, 14);
            this.lblCurrentPipeline.Text = "Не выбран";

            this.lblInspectionCount.AutoSize = true;
            this.lblInspectionCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInspectionCount.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblInspectionCount.Location = new System.Drawing.Point(450, 14);
            this.lblInspectionCount.Text = "Выберите трубопровод";

            this.infoPanel.Controls.Add(this.lblCurrentPipelineTitle);
            this.infoPanel.Controls.Add(this.lblCurrentPipeline);
            this.infoPanel.Controls.Add(this.lblInspectionCount);

            // ========== DATA GRID VIEW ==========
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ReadOnly = true;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Padding = new System.Windows.Forms.Padding(0);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);

            // Стиль заголовков
            this.dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridView.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView.ColumnHeadersHeight = 35;

            // Стиль ячеек
            this.dataGridView.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(32, 32, 32);
            this.dataGridView.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.dataGridView.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridView.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);

            // Стиль строк
            this.dataGridView.RowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.dataGridView.RowTemplate.Height = 32;

            // ========== BUTTON PANEL ==========
            this.buttonPanel.BackColor = System.Drawing.Color.White;
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Height = 60;
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);

            // Кнопки в правильном порядке: Добавить, Изменить, Удалить, Импорт, Обновить
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Size = new System.Drawing.Size(110, 38);
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEdit.Text = "Изменить";
            this.btnEdit.Size = new System.Drawing.Size(110, 38);
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.Enabled = false;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.Text = "Удалить";
            this.btnDelete.Size = new System.Drawing.Size(110, 38);
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.Enabled = false;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnImport.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnImport.Text = "Импорт из Excel";
            this.btnImport.Size = new System.Drawing.Size(130, 38);
            this.btnImport.FlatAppearance.BorderSize = 1;
            this.btnImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);

            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.Size = new System.Drawing.Size(100, 38);
            this.btnRefresh.FlatAppearance.BorderSize = 1;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Click += (s, e) => LoadInspections();

            // Размещение кнопок через FlowLayoutPanel слева направо
            var flowButtons = new System.Windows.Forms.FlowLayoutPanel();
            flowButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            flowButtons.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            flowButtons.Controls.Add(this.btnAdd);
            flowButtons.Controls.Add(this.btnEdit);
            flowButtons.Controls.Add(this.btnDelete);
            flowButtons.Controls.Add(this.btnImport);
            flowButtons.Controls.Add(this.btnRefresh);

            this.buttonPanel.Controls.Add(flowButtons);

            // ========== STATUS PANEL ==========
            this.statusPanel.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.statusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusPanel.Height = 30;
            this.statusPanel.Padding = new System.Windows.Forms.Padding(20, 5, 20, 5);

            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            this.lblStatus.Text = "Двойной клик по строке для редактирования | Поддерживается импорт из Excel";

            this.statusPanel.Controls.Add(this.lblStatus);

            // ========== MAIN FORM ==========
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.infoPanel);
            this.Controls.Add(this.selectionPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.statusPanel);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Text = "Управление инспекциями";
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.headerPanel.ResumeLayout(false);
            this.selectionPanel.ResumeLayout(false);
            this.infoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.statusPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // Компоненты
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;

        private System.Windows.Forms.Panel selectionPanel;
        private System.Windows.Forms.ComboBox cmbPipeline;
        private System.Windows.Forms.Label lblPipeline;
        private System.Windows.Forms.Label lblPipelineCount;

        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.Label lblCurrentPipelineTitle;
        private System.Windows.Forms.Label lblCurrentPipeline;
        private System.Windows.Forms.Label lblInspectionCount;

        private System.Windows.Forms.DataGridView dataGridView;

        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnRefresh;

        private System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Label lblStatus;
    }
}