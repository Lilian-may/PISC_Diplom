namespace Pipe
{
    partial class InspectionEditForm
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

            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.cmbToolType = new System.Windows.Forms.ComboBox();
            this.lblToolType = new System.Windows.Forms.Label();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.txtCoverage = new System.Windows.Forms.TextBox();
            this.lblCoverage = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();

            this.headerPanel.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();

            // ========== HEADER PANEL ==========
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(0, 70, 128);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Height = 50;
            this.headerPanel.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);

            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(20, 12);
            this.titleLabel.Text = "Инспекция";

            this.headerPanel.Controls.Add(this.titleLabel);

            // ========== TABLE LAYOUT PANEL ==========
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.RowCount = 6;
            this.tableLayoutPanel.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));

            // Row 0 - Дата инспекции
            this.lblDate.Text = "Дата инспекции:";
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Row 1 - Тип прибора
            this.lblToolType.Text = "Тип прибора:";
            this.lblToolType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblToolType.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.cmbToolType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToolType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbToolType.Items.AddRange(new object[] { "MFL", "UT", "COMBO" });

            // Row 2 - Скорость (м/с)
            this.lblSpeed.Text = "Скорость (м/с):";
            this.lblSpeed.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSpeed.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.txtSpeed.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSpeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Row 3 - Охват (%)
            this.lblCoverage.Text = "Охват (%):";
            this.lblCoverage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCoverage.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.txtCoverage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCoverage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCoverage.Text = "100";

            // Row 4 - Статус
            this.lblStatus.Text = "Статус:";
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.txtStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStatus.Text = "Выполнена";

            // Добавление строк в таблицу
            this.tableLayoutPanel.Controls.Add(this.lblDate, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.dtpDate, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.lblToolType, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.cmbToolType, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.lblSpeed, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.txtSpeed, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.lblCoverage, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.txtCoverage, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.lblStatus, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.txtStatus, 1, 4);

            // ========== BUTTON PANEL ==========
            var buttonPanel = new System.Windows.Forms.Panel();
            buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            buttonPanel.Height = 60;
            buttonPanel.BackColor = System.Drawing.Color.White;
            buttonPanel.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);

            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Text = "Сохранить";
            this.btnSave.Size = new System.Drawing.Size(120, 38);
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;

            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Size = new System.Drawing.Size(120, 38);
            this.btnCancel.FlatAppearance.BorderSize = 1;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;

            var flowButtons = new System.Windows.Forms.FlowLayoutPanel();
            flowButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            flowButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            flowButtons.Controls.Add(this.btnSave);
            flowButtons.Controls.Add(this.btnCancel);

            buttonPanel.Controls.Add(flowButtons);

            // ========== MAIN FORM ==========
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 350);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(buttonPanel);
            this.Controls.Add(this.headerPanel);
            this.MinimumSize = new System.Drawing.Size(450, 350);
            this.MaximumSize = new System.Drawing.Size(550, 450);
            this.Text = "Редактирование инспекции";
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            this.headerPanel.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.ComboBox cmbToolType;
        private System.Windows.Forms.Label lblToolType;
        private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.TextBox txtCoverage;
        private System.Windows.Forms.Label lblCoverage;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}