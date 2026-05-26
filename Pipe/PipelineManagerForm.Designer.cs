namespace Pipe
{
    partial class PipelineManagerForm
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

            this.infoPanel = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();

            this.dataGridView = new System.Windows.Forms.DataGridView();

            this.buttonPanel = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();

            this.statusPanel = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();

            this.headerPanel.SuspendLayout();
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
            this.titleLabel.Text = "Управление трубопроводами";

            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.subtitleLabel.Location = new System.Drawing.Point(22, 40);
            this.subtitleLabel.Text = "Просмотр и управление магистральными газопроводами";

            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.subtitleLabel);

            // ========== INFO PANEL ==========
            this.infoPanel.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoPanel.Height = 45;
            this.infoPanel.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);

            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblCount.Location = new System.Drawing.Point(20, 12);
            this.lblCount.Text = "Список трубопроводов";

            this.infoPanel.Controls.Add(this.lblCount);

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
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;

            // Стиль заголовков
            this.dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridView.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView.ColumnHeadersHeight = 40;

            // Стиль ячеек
            this.dataGridView.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dataGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(32, 32, 32);
            this.dataGridView.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.dataGridView.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridView.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);

            // Стиль строк
            this.dataGridView.RowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.dataGridView.RowTemplate.Height = 35;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.None;

            // ========== BUTTON PANEL ==========
            this.buttonPanel.BackColor = System.Drawing.Color.White;
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Height = 60;
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);

            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Size = new System.Drawing.Size(110, 38);
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;

            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEdit.Text = "Изменить";
            this.btnEdit.Size = new System.Drawing.Size(110, 38);
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.Enabled = false;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;

            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.Text = "Удалить";
            this.btnDelete.Size = new System.Drawing.Size(110, 38);
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.Enabled = false;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;

            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnImport.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnImport.Text = "Импорт из Excel";
            this.btnImport.Size = new System.Drawing.Size(130, 38);
            this.btnImport.FlatAppearance.BorderSize = 1;
            this.btnImport.Cursor = System.Windows.Forms.Cursors.Hand;

            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.Size = new System.Drawing.Size(100, 38);
            this.btnRefresh.FlatAppearance.BorderSize = 1;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;

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
            this.ClientSize = new System.Drawing.Size(1200, 650);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.infoPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.statusPanel);
            this.MinimumSize = new System.Drawing.Size(1000, 500);
            this.Text = "Управление трубопроводами";
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.headerPanel.ResumeLayout(false);
            this.infoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.statusPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.Label lblCount;
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