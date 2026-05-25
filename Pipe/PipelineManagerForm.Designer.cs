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
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnImport = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();

			this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.ReadOnly = true;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.MultiSelect = false;

			var flow = new System.Windows.Forms.FlowLayoutPanel();
			flow.Dock = System.Windows.Forms.DockStyle.Bottom;
			flow.Height = 50;
			flow.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			flow.Controls.Add(this.btnRefresh);
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

			this.btnRefresh.Text = "Обновить";
			this.btnRefresh.Size = new System.Drawing.Size(100, 30);
			this.btnRefresh.Click += (s, e) => RefreshData();

			this.btnImport.Text = "Импорт из Excel";
			this.btnImport.Size = new System.Drawing.Size(120, 30);
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);

			this.Controls.Add(dataGridView);
			this.Controls.Add(flow);
			this.ClientSize = new System.Drawing.Size(900, 500);
			this.Text = "Управление трубопроводами";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);
		}

		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnImport;
	}
}