using System.Drawing;
using System.Windows.Forms;

namespace Pipe
{
	partial class ImportPipelinesForm
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
			this.lblInstruction = new Label();
			this.txtFilePath = new TextBox();
			this.btnSelectFile = new Button();
			this.btnImport = new Button();
			this.btnCancel = new Button();
			this.lblStatus = new Label();
			this.SuspendLayout();

			this.lblInstruction.AutoSize = true;
			this.lblInstruction.Location = new Point(12, 15);
			this.lblInstruction.Text = "Выберите файл Excel/CSV с колонками: name, start_km, end_km, diameter_mm, wall_thickness_mm, steel_grade, yield_strength_mpa, design_pressure_mpa, operating_pressure_mpa, region, commissioning_date";
			this.lblInstruction.MaximumSize = new Size(500, 0);

			this.txtFilePath.Location = new Point(12, 50);
			this.txtFilePath.Size = new Size(400, 20);

			this.btnSelectFile.Location = new Point(420, 48);
			this.btnSelectFile.Size = new Size(100, 25);
			this.btnSelectFile.Text = "Обзор...";
			this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);

			this.btnImport.Location = new Point(120, 90);
			this.btnImport.Size = new Size(100, 35);
			this.btnImport.Text = "Импорт";
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);

			this.btnCancel.Location = new Point(240, 90);
			this.btnCancel.Size = new Size(100, 35);
			this.btnCancel.Text = "Отмена";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new Point(12, 140);
			this.lblStatus.Text = "Готов к импорту";

			this.ClientSize = new Size(550, 180);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnImport);
			this.Controls.Add(this.btnSelectFile);
			this.Controls.Add(this.txtFilePath);
			this.Controls.Add(this.lblInstruction);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Импорт трубопроводов";
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private Label lblInstruction;
		private TextBox txtFilePath;
		private Button btnSelectFile;
		private Button btnImport;
		private Button btnCancel;
		private Label lblStatus;
	}
}