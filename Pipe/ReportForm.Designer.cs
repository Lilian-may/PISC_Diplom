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
			this.cmbPipeline = new System.Windows.Forms.ComboBox();
			this.cmbInspection = new System.Windows.Forms.ComboBox();
			this.lblPipeline = new System.Windows.Forms.Label();
			this.lblInspection = new System.Windows.Forms.Label();
			this.btnGeneratePdf = new System.Windows.Forms.Button();
			this.btnExportExcel = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.SuspendLayout();

			this.lblPipeline.AutoSize = true;
			this.lblPipeline.Location = new System.Drawing.Point(20, 30);
			this.lblPipeline.Text = "Трубопровод:";

			this.cmbPipeline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPipeline.Location = new System.Drawing.Point(130, 27);
			this.cmbPipeline.Size = new System.Drawing.Size(280, 21);
			this.cmbPipeline.SelectedIndexChanged += new System.EventHandler(this.cmbPipeline_SelectedIndexChanged);

			this.lblInspection.AutoSize = true;
			this.lblInspection.Location = new System.Drawing.Point(20, 70);
			this.lblInspection.Text = "Инспекция:";

			this.cmbInspection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbInspection.Location = new System.Drawing.Point(130, 67);
			this.cmbInspection.Size = new System.Drawing.Size(280, 21);
			this.cmbInspection.Enabled = false;

			this.btnGeneratePdf.Location = new System.Drawing.Point(50, 120);
			this.btnGeneratePdf.Size = new System.Drawing.Size(180, 40);
			this.btnGeneratePdf.Text = "Сформировать PDF (критические)";
			this.btnGeneratePdf.Enabled = false;
			this.btnGeneratePdf.Click += new System.EventHandler(this.btnGeneratePdf_Click);

			this.btnExportExcel.Location = new System.Drawing.Point(250, 120);
			this.btnExportExcel.Size = new System.Drawing.Size(160, 40);
			this.btnExportExcel.Text = "Экспорт Excel (все дефекты)";
			this.btnExportExcel.Enabled = false;
			this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(20, 180);
			this.lblStatus.Text = "Выберите трубопровод";
			this.lblStatus.ForeColor = System.Drawing.Color.Gray;

			this.ClientSize = new System.Drawing.Size(460, 220);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.btnExportExcel);
			this.Controls.Add(this.btnGeneratePdf);
			this.Controls.Add(this.cmbInspection);
			this.Controls.Add(this.lblInspection);
			this.Controls.Add(this.cmbPipeline);
			this.Controls.Add(this.lblPipeline);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Генерация отчётов";
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private System.Windows.Forms.ComboBox cmbPipeline;
		private System.Windows.Forms.ComboBox cmbInspection;
		private System.Windows.Forms.Label lblPipeline;
		private System.Windows.Forms.Label lblInspection;
		private System.Windows.Forms.Button btnGeneratePdf;
		private System.Windows.Forms.Button btnExportExcel;
		private System.Windows.Forms.Label lblStatus;
	}
}