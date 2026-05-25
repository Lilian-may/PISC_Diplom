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
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblToolType = new System.Windows.Forms.Label();
            this.cmbToolType = new System.Windows.Forms.ComboBox();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.lblCoverage = new System.Windows.Forms.Label();
            this.txtCoverage = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblDate.AutoSize = true; this.lblDate.Location = new System.Drawing.Point(20, 20); this.lblDate.Text = "Дата инспекции:*";
            this.dtpDate.Location = new System.Drawing.Point(150, 17); this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.lblToolType.AutoSize = true; this.lblToolType.Location = new System.Drawing.Point(20, 60); this.lblToolType.Text = "Тип прибора:*";
            this.cmbToolType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToolType.Items.AddRange(new object[] { "MFL", "UT", "COMBO" });
            this.cmbToolType.Location = new System.Drawing.Point(150, 57); this.cmbToolType.Size = new System.Drawing.Size(120, 21);
            this.lblSpeed.AutoSize = true; this.lblSpeed.Location = new System.Drawing.Point(20, 100); this.lblSpeed.Text = "Скорость (м/с):";
            this.txtSpeed.Location = new System.Drawing.Point(150, 97); this.txtSpeed.Size = new System.Drawing.Size(100, 20);
            this.lblCoverage.AutoSize = true; this.lblCoverage.Location = new System.Drawing.Point(20, 140); this.lblCoverage.Text = "Охват (%):";
            this.txtCoverage.Location = new System.Drawing.Point(150, 137); this.txtCoverage.Text = "100"; this.txtCoverage.Size = new System.Drawing.Size(80, 20);
            this.lblStatus.AutoSize = true; this.lblStatus.Location = new System.Drawing.Point(20, 180); this.lblStatus.Text = "Статус:";
            this.txtStatus.Location = new System.Drawing.Point(150, 177); this.txtStatus.Text = "Выполнена"; this.txtStatus.Size = new System.Drawing.Size(150, 20);
            this.btnSave.Location = new System.Drawing.Point(80, 230); this.btnSave.Size = new System.Drawing.Size(100, 30); this.btnSave.Text = "Сохранить"; this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnCancel.Location = new System.Drawing.Point(200, 230); this.btnCancel.Size = new System.Drawing.Size(100, 30); this.btnCancel.Text = "Отмена"; this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.ClientSize = new System.Drawing.Size(380, 300);
            this.Controls.Add(this.btnCancel); this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtStatus); this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtCoverage); this.Controls.Add(this.lblCoverage);
            this.Controls.Add(this.txtSpeed); this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.cmbToolType); this.Controls.Add(this.lblToolType);
            this.Controls.Add(this.dtpDate); this.Controls.Add(this.lblDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Инспекция";
            this.ResumeLayout(false); this.PerformLayout();
        }

        private System.Windows.Forms.Label lblDate; private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblToolType; private System.Windows.Forms.ComboBox cmbToolType;
        private System.Windows.Forms.Label lblSpeed; private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.Label lblCoverage; private System.Windows.Forms.TextBox txtCoverage;
        private System.Windows.Forms.Label lblStatus; private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnSave; private System.Windows.Forms.Button btnCancel;
    }
}