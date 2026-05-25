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
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblStartKm = new System.Windows.Forms.Label();
            this.txtStartKm = new System.Windows.Forms.TextBox();
            this.lblEndKm = new System.Windows.Forms.Label();
            this.txtEndKm = new System.Windows.Forms.TextBox();
            this.lblDiameter = new System.Windows.Forms.Label();
            this.txtDiameter = new System.Windows.Forms.TextBox();
            this.lblWallThickness = new System.Windows.Forms.Label();
            this.txtWallThickness = new System.Windows.Forms.TextBox();
            this.lblSteelGrade = new System.Windows.Forms.Label();
            this.txtSteelGrade = new System.Windows.Forms.TextBox();
            this.lblYieldStrength = new System.Windows.Forms.Label();
            this.txtYieldStrength = new System.Windows.Forms.TextBox();
            this.lblDesignPressure = new System.Windows.Forms.Label();
            this.txtDesignPressure = new System.Windows.Forms.TextBox();
            this.lblOperatingPressure = new System.Windows.Forms.Label();
            this.txtOperatingPressure = new System.Windows.Forms.TextBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.txtRegion = new System.Windows.Forms.TextBox();
            this.lblCommissioning = new System.Windows.Forms.Label();
            this.dtpCommissioning = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblName.AutoSize = true; this.lblName.Location = new System.Drawing.Point(20, 20); this.lblName.Text = "Наименование:*";
            this.txtName.Location = new System.Drawing.Point(150, 17); this.txtName.Size = new System.Drawing.Size(250, 20);
            this.lblStartKm.AutoSize = true; this.lblStartKm.Location = new System.Drawing.Point(20, 50); this.lblStartKm.Text = "Начало (км):";
            this.txtStartKm.Location = new System.Drawing.Point(150, 47); this.txtStartKm.Size = new System.Drawing.Size(100, 20);
            this.lblEndKm.AutoSize = true; this.lblEndKm.Location = new System.Drawing.Point(20, 80); this.lblEndKm.Text = "Конец (км):*";
            this.txtEndKm.Location = new System.Drawing.Point(150, 77); this.txtEndKm.Size = new System.Drawing.Size(100, 20);
            this.lblDiameter.AutoSize = true; this.lblDiameter.Location = new System.Drawing.Point(20, 110); this.lblDiameter.Text = "Диаметр (мм):*";
            this.txtDiameter.Location = new System.Drawing.Point(150, 107); this.txtDiameter.Size = new System.Drawing.Size(100, 20);
            this.lblWallThickness.AutoSize = true; this.lblWallThickness.Location = new System.Drawing.Point(20, 140); this.lblWallThickness.Text = "Толщина стенки (мм):*";
            this.txtWallThickness.Location = new System.Drawing.Point(150, 137); this.txtWallThickness.Size = new System.Drawing.Size(100, 20);
            this.lblSteelGrade.AutoSize = true; this.lblSteelGrade.Location = new System.Drawing.Point(20, 170); this.lblSteelGrade.Text = "Марка стали:";
            this.txtSteelGrade.Location = new System.Drawing.Point(150, 167); this.txtSteelGrade.Size = new System.Drawing.Size(150, 20);
            this.lblYieldStrength.AutoSize = true; this.lblYieldStrength.Location = new System.Drawing.Point(20, 200); this.lblYieldStrength.Text = "Предел текучести (МПа):*";
            this.txtYieldStrength.Location = new System.Drawing.Point(150, 197); this.txtYieldStrength.Size = new System.Drawing.Size(100, 20);
            this.lblDesignPressure.AutoSize = true; this.lblDesignPressure.Location = new System.Drawing.Point(20, 230); this.lblDesignPressure.Text = "Проектное давление (МПа):*";
            this.txtDesignPressure.Location = new System.Drawing.Point(150, 227); this.txtDesignPressure.Size = new System.Drawing.Size(100, 20);
            this.lblOperatingPressure.AutoSize = true; this.lblOperatingPressure.Location = new System.Drawing.Point(20, 260); this.lblOperatingPressure.Text = "Рабочее давление (МПа):*";
            this.txtOperatingPressure.Location = new System.Drawing.Point(150, 257); this.txtOperatingPressure.Size = new System.Drawing.Size(100, 20);
            this.lblRegion.AutoSize = true; this.lblRegion.Location = new System.Drawing.Point(20, 290); this.lblRegion.Text = "Регион:";
            this.txtRegion.Location = new System.Drawing.Point(150, 287); this.txtRegion.Size = new System.Drawing.Size(250, 20);
            this.lblCommissioning.AutoSize = true; this.lblCommissioning.Location = new System.Drawing.Point(20, 320); this.lblCommissioning.Text = "Дата ввода:";
            this.dtpCommissioning.Location = new System.Drawing.Point(150, 317); this.dtpCommissioning.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.btnSave.Location = new System.Drawing.Point(120, 360); this.btnSave.Size = new System.Drawing.Size(100, 30); this.btnSave.Text = "Сохранить"; this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnCancel.Location = new System.Drawing.Point(240, 360); this.btnCancel.Size = new System.Drawing.Size(100, 30); this.btnCancel.Text = "Отмена"; this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.ClientSize = new System.Drawing.Size(450, 420);
            this.Controls.Add(this.btnCancel); this.Controls.Add(this.btnSave); this.Controls.Add(this.dtpCommissioning);
            this.Controls.Add(this.lblCommissioning); this.Controls.Add(this.txtRegion); this.Controls.Add(this.lblRegion);
            this.Controls.Add(this.txtOperatingPressure); this.Controls.Add(this.lblOperatingPressure);
            this.Controls.Add(this.txtDesignPressure); this.Controls.Add(this.lblDesignPressure);
            this.Controls.Add(this.txtYieldStrength); this.Controls.Add(this.lblYieldStrength);
            this.Controls.Add(this.txtSteelGrade); this.Controls.Add(this.lblSteelGrade);
            this.Controls.Add(this.txtWallThickness); this.Controls.Add(this.lblWallThickness);
            this.Controls.Add(this.txtDiameter); this.Controls.Add(this.lblDiameter);
            this.Controls.Add(this.txtEndKm); this.Controls.Add(this.lblEndKm);
            this.Controls.Add(this.txtStartKm); this.Controls.Add(this.lblStartKm);
            this.Controls.Add(this.txtName); this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Трубопровод";
            this.ResumeLayout(false); this.PerformLayout();
        }

        private System.Windows.Forms.Label lblName; private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblStartKm; private System.Windows.Forms.TextBox txtStartKm;
        private System.Windows.Forms.Label lblEndKm; private System.Windows.Forms.TextBox txtEndKm;
        private System.Windows.Forms.Label lblDiameter; private System.Windows.Forms.TextBox txtDiameter;
        private System.Windows.Forms.Label lblWallThickness; private System.Windows.Forms.TextBox txtWallThickness;
        private System.Windows.Forms.Label lblSteelGrade; private System.Windows.Forms.TextBox txtSteelGrade;
        private System.Windows.Forms.Label lblYieldStrength; private System.Windows.Forms.TextBox txtYieldStrength;
        private System.Windows.Forms.Label lblDesignPressure; private System.Windows.Forms.TextBox txtDesignPressure;
        private System.Windows.Forms.Label lblOperatingPressure; private System.Windows.Forms.TextBox txtOperatingPressure;
        private System.Windows.Forms.Label lblRegion; private System.Windows.Forms.TextBox txtRegion;
        private System.Windows.Forms.Label lblCommissioning; private System.Windows.Forms.DateTimePicker dtpCommissioning;
        private System.Windows.Forms.Button btnSave; private System.Windows.Forms.Button btnCancel;
    }
}