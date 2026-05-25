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
			this.cmbPipeline = new System.Windows.Forms.ComboBox();
			this.lblPipeline = new System.Windows.Forms.Label();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnImport = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();

			this.lblPipeline.AutoSize = true;
			this.lblPipeline.Location = new System.Drawing.Point(12, 15);
			this.lblPipeline.Text = "Трубопровод:";

			this.cmbPipeline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPipeline.Location = new System.Drawing.Point(120, 12);
			this.cmbPipeline.Size = new System.Drawing.Size(300, 21);
			this.cmbPipeline.SelectedIndexChanged += new System.EventHandler(this.cmbPipeline_SelectedIndexChanged);

			this.dataGridView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.dataGridView.Location = new System.Drawing.Point(12, 50);
			this.dataGridView.Size = new System.Drawing.Size(860, 380);
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.ReadOnly = true;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

			var flow = new System.Windows.Forms.FlowLayoutPanel();
			flow.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			flow.Location = new System.Drawing.Point(12, 440);
			flow.Size = new System.Drawing.Size(860, 40);
			flow.Controls.Add(this.btnDelete);
			flow.Controls.Add(this.btnEdit);
			flow.Controls.Add(this.btnAdd);
			flow.Controls.Add(this.btnImport);

			this.btnAdd.Text = "Добавить";
			this.btnAdd.Size = new System.Drawing.Size(100, 30);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

			this.btnEdit.Text = "Изменить";
			this.btnEdit.Size = new System.Drawing.Size(100, 30);
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

			this.btnDelete.Text = "Удалить";
			this.btnDelete.Size = new System.Drawing.Size(100, 30);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

			this.btnImport.Text = "Импорт из Excel";
			this.btnImport.Size = new System.Drawing.Size(120, 30);
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);

			this.Controls.Add(flow);
			this.Controls.Add(this.dataGridView);
			this.Controls.Add(this.cmbPipeline);
			this.Controls.Add(this.lblPipeline);
			this.ClientSize = new System.Drawing.Size(900, 500);
			this.Text = "Управление инспекциями";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private System.Windows.Forms.ComboBox cmbPipeline;
		private System.Windows.Forms.Label lblPipeline;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnImport;
	}
}