namespace Pipe
{
	partial class ImportDataForm
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
			this.btnImportPipelines = new System.Windows.Forms.Button();
			this.btnImportInspections = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();

			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(30, 20);
			this.label1.Text = "Импорт трубопроводов";
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(30, 80);
			this.label2.Text = "Импорт инспекций";

			this.btnImportPipelines.Location = new System.Drawing.Point(30, 45);
			this.btnImportPipelines.Size = new System.Drawing.Size(200, 30);
			this.btnImportPipelines.Text = "Загрузить из Excel/CSV";
			this.btnImportPipelines.Click += new System.EventHandler(this.btnImportPipelines_Click);

			this.btnImportInspections.Location = new System.Drawing.Point(30, 105);
			this.btnImportInspections.Size = new System.Drawing.Size(200, 30);
			this.btnImportInspections.Text = "Загрузить из Excel/CSV";
			this.btnImportInspections.Click += new System.EventHandler(this.btnImportInspections_Click);

			this.btnClose.Location = new System.Drawing.Point(130, 160);
			this.btnClose.Size = new System.Drawing.Size(100, 30);
			this.btnClose.Text = "Закрыть";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

			this.ClientSize = new System.Drawing.Size(280, 220);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnImportInspections);
			this.Controls.Add(this.btnImportPipelines);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Импорт справочных данных";
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private System.Windows.Forms.Button btnImportPipelines;
		private System.Windows.Forms.Button btnImportInspections;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}