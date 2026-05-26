namespace Pipe
{
    partial class PipelineEditForm
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

            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtStartKm = new System.Windows.Forms.TextBox();
            this.lblStartKm = new System.Windows.Forms.Label();
            this.txtEndKm = new System.Windows.Forms.TextBox();
            this.lblEndKm = new System.Windows.Forms.Label();
            this.txtDiameter = new System.Windows.Forms.TextBox();
            this.lblDiameter = new System.Windows.Forms.Label();
            this.txtWallThickness = new System.Windows.Forms.TextBox();
            this.lblWallThickness = new System.Windows.Forms.Label();
            this.txtSteelGrade = new System.Windows.Forms.TextBox();
            this.lblSteelGrade = new System.Windows.Forms.Label();
            this.txtYieldStrength = new System.Windows.Forms.TextBox();
            this.lblYieldStrength = new System.Windows.Forms.Label();
            this.txtDesignPressure = new System.Windows.Forms.TextBox();
            this.lblDesignPressure = new System.Windows.Forms.Label();
            this.txtOperatingPressure = new System.Windows.Forms.TextBox();
            this.lblOperatingPressure = new System.Windows.Forms.Label();
            this.txtRegion = new System.Windows.Forms.TextBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.dtpCommissioning = new System.Windows.Forms.DateTimePicker();
            this.lblCommissioning = new System.Windows.Forms.Label();

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
            this.titleLabel.Text = "Трубопровод";

            this.headerPanel.Controls.Add(this.titleLabel);

            // ========== TABLE LAYOUT PANEL ==========
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.RowCount = 12;
            this.tableLayoutPanel.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));

            // Row 0 - Наименование
            this.lblName.Text = "Наименование:";
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Row 1 - Начало (км)
            this.lblStartKm.Text = "Начало (км):";
            this.lblStartKm.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStartKm.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.txtStartKm.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtStartKm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Row 2 - Конец (км)
            this.lblEndKm.Text = "Конец (км):";
            this.lblEndKm.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEndKm.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.txtEndKm.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEndKm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Row 3 - Диаметр (мм)
            this.lblDiameter.Text = "Диаметр (мм):";
            this.lblDiameter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiameter.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.txtDiameter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDiameter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Row 4 - Толщина стенки (мм)
            this.lblWallThickness.Text = "Толщина стенки (мм):";
            this.lblWallThickness.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWallThickness.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.txtWallThickness.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtWallThickness.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Row 5 - Марка стали
            this.lblSteelGrade.Text = "Марка стали:";
            this.lblSteelGrade.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSteelGrade.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.txtSteelGrade.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSteelGrade.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Row 6 - Предел текучести (МПа)
            this.lblYieldStrength.Text = "Предел текучести (МПа):";
            this.lblYieldStrength.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblYieldStrength.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.txtYieldStrength.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtYieldStrength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Row 7 - Проектное давление (МПа)
            this.lblDesignPressure.Text = "Проектное давление (МПа):";
            this.lblDesignPressure.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDesignPressure.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.txtDesignPressure.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDesignPressure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Row 8 - Рабочее давление (МПа)
            this.lblOperatingPressure.Text = "Рабочее давление (МПа):";
            this.lblOperatingPressure.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOperatingPressure.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.txtOperatingPressure.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtOperatingPressure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Row 9 - Регион
            this.lblRegion.Text = "Регион:";
            this.lblRegion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRegion.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.txtRegion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRegion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Row 10 - Дата ввода
            this.lblCommissioning.Text = "Дата ввода:";
            this.lblCommissioning.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCommissioning.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.dtpCommissioning.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCommissioning.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Добавление строк в таблицу
            this.tableLayoutPanel.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.txtName, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.lblStartKm, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.txtStartKm, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.lblEndKm, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.txtEndKm, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.lblDiameter, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.txtDiameter, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.lblWallThickness, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.txtWallThickness, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.lblSteelGrade, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.txtSteelGrade, 1, 5);
            this.tableLayoutPanel.Controls.Add(this.lblYieldStrength, 0, 6);
            this.tableLayoutPanel.Controls.Add(this.txtYieldStrength, 1, 6);
            this.tableLayoutPanel.Controls.Add(this.lblDesignPressure, 0, 7);
            this.tableLayoutPanel.Controls.Add(this.txtDesignPressure, 1, 7);
            this.tableLayoutPanel.Controls.Add(this.lblOperatingPressure, 0, 8);
            this.tableLayoutPanel.Controls.Add(this.txtOperatingPressure, 1, 8);
            this.tableLayoutPanel.Controls.Add(this.lblRegion, 0, 9);
            this.tableLayoutPanel.Controls.Add(this.txtRegion, 1, 9);
            this.tableLayoutPanel.Controls.Add(this.lblCommissioning, 0, 10);
            this.tableLayoutPanel.Controls.Add(this.dtpCommissioning, 1, 10);

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
            this.ClientSize = new System.Drawing.Size(550, 500);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(buttonPanel);
            this.Controls.Add(this.headerPanel);
            this.MinimumSize = new System.Drawing.Size(550, 500);
            this.MaximumSize = new System.Drawing.Size(650, 600);
            this.Text = "Редактирование трубопровода";
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            this.headerPanel.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtStartKm;
        private System.Windows.Forms.Label lblStartKm;
        private System.Windows.Forms.TextBox txtEndKm;
        private System.Windows.Forms.Label lblEndKm;
        private System.Windows.Forms.TextBox txtDiameter;
        private System.Windows.Forms.Label lblDiameter;
        private System.Windows.Forms.TextBox txtWallThickness;
        private System.Windows.Forms.Label lblWallThickness;
        private System.Windows.Forms.TextBox txtSteelGrade;
        private System.Windows.Forms.Label lblSteelGrade;
        private System.Windows.Forms.TextBox txtYieldStrength;
        private System.Windows.Forms.Label lblYieldStrength;
        private System.Windows.Forms.TextBox txtDesignPressure;
        private System.Windows.Forms.Label lblDesignPressure;
        private System.Windows.Forms.TextBox txtOperatingPressure;
        private System.Windows.Forms.Label lblOperatingPressure;
        private System.Windows.Forms.TextBox txtRegion;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.DateTimePicker dtpCommissioning;
        private System.Windows.Forms.Label lblCommissioning;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}