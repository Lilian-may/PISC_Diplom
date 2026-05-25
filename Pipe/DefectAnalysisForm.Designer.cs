namespace Pipe
{
	partial class DefectAnalysisForm
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
			this.cmbInspection = new System.Windows.Forms.ComboBox();
			this.dataGridViewDefects = new System.Windows.Forms.DataGridView();
			this.btnImport = new System.Windows.Forms.Button();
			this.btnFilterCritical = new System.Windows.Forms.Button();
			this.btnResetFilter = new System.Windows.Forms.Button();
			this.btnRecalc = new System.Windows.Forms.Button();
			this.lblPipeInfo = new System.Windows.Forms.Label();
			this.lblProgress = new System.Windows.Forms.Label();
			this.panelCanvas = new System.Windows.Forms.Panel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabTable = new System.Windows.Forms.TabPage();
			this.tabCanvas = new System.Windows.Forms.TabPage();
			this.tabControl1.SuspendLayout();
			this.tabTable.SuspendLayout();
			this.tabCanvas.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewDefects)).BeginInit();
			this.SuspendLayout();

			this.cmbPipeline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPipeline.Location = new System.Drawing.Point(12, 12);
			this.cmbPipeline.Size = new System.Drawing.Size(200, 21);
			this.cmbPipeline.SelectedIndexChanged += new System.EventHandler(this.cmbPipeline_SelectedIndexChanged);

			this.cmbInspection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbInspection.Location = new System.Drawing.Point(230, 12);
			this.cmbInspection.Size = new System.Drawing.Size(150, 21);
			this.cmbInspection.SelectedIndexChanged += new System.EventHandler(this.cmbInspection_SelectedIndexChanged);

			this.btnImport.Location = new System.Drawing.Point(400, 10);
			this.btnImport.Size = new System.Drawing.Size(100, 25);
			this.btnImport.Text = "Импорт CSV";
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);

			this.btnFilterCritical.Location = new System.Drawing.Point(510, 10);
			this.btnFilterCritical.Size = new System.Drawing.Size(120, 25);
			this.btnFilterCritical.Text = "Только High/Critical";
			this.btnFilterCritical.Click += new System.EventHandler(this.btnFilterCritical_Click);

			this.btnResetFilter.Location = new System.Drawing.Point(640, 10);
			this.btnResetFilter.Size = new System.Drawing.Size(100, 25);
			this.btnResetFilter.Text = "Сброс фильтра";
			this.btnResetFilter.Click += new System.EventHandler(this.btnResetFilter_Click);

			this.btnRecalc.Location = new System.Drawing.Point(750, 10);
			this.btnRecalc.Size = new System.Drawing.Size(120, 25);
			this.btnRecalc.Text = "Пересчитать ERF";
			this.btnRecalc.Click += new System.EventHandler(this.btnRecalc_Click);

			this.lblPipeInfo.AutoSize = true;
			this.lblPipeInfo.Location = new System.Drawing.Point(12, 45);
			this.lblPipeInfo.Size = new System.Drawing.Size(300, 13);

			this.lblProgress.AutoSize = true;
			this.lblProgress.Location = new System.Drawing.Point(12, 550);
			this.lblProgress.Size = new System.Drawing.Size(0, 13);

			this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.tabControl1.Location = new System.Drawing.Point(12, 70);
			this.tabControl1.Size = new System.Drawing.Size(860, 470);
			this.tabControl1.Controls.Add(this.tabTable);
			this.tabControl1.Controls.Add(this.tabCanvas);

			this.tabTable.Text = "Таблица дефектов";
			this.tabCanvas.Text = "Развёртка трубы";

			this.dataGridViewDefects.Dock = DockStyle.Fill;
			this.dataGridViewDefects.AllowUserToAddRows = false;
			this.dataGridViewDefects.ReadOnly = true;
			this.dataGridViewDefects.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.tabTable.Controls.Add(this.dataGridViewDefects);

			this.panelCanvas.Dock = DockStyle.Fill;
			this.panelCanvas.BackColor = System.Drawing.Color.White;
			this.tabCanvas.Controls.Add(this.panelCanvas);

			this.ClientSize = new System.Drawing.Size(900, 600);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.lblProgress);
			this.Controls.Add(this.lblPipeInfo);
			this.Controls.Add(this.btnRecalc);
			this.Controls.Add(this.btnResetFilter);
			this.Controls.Add(this.btnFilterCritical);
			this.Controls.Add(this.btnImport);
			this.Controls.Add(this.cmbInspection);
			this.Controls.Add(this.cmbPipeline);
			this.Text = "Анализ дефектов ВТД";
			this.tabControl1.ResumeLayout(false);
			this.tabTable.ResumeLayout(false);
			this.tabCanvas.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewDefects)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

			this.btnResetView = new System.Windows.Forms.Button();
			this.btnResetView.Location = new System.Drawing.Point(12, 570);
			this.btnResetView.Size = new System.Drawing.Size(100, 30);
			this.btnResetView.Text = "Сброс вида";
			this.btnResetView.Click += (s, e) => pipeCanvas.ResetView();
			this.btnToggleGrid = new System.Windows.Forms.Button();
			this.btnToggleGrid.Location = new System.Drawing.Point(120, 570);
			this.btnToggleGrid.Size = new System.Drawing.Size(100, 30);
			this.btnToggleGrid.Text = "Сетка: Вкл";
			this.btnToggleGrid.Click += (s, e) =>
			{
				pipeCanvas.ToggleGrid();
				btnToggleGrid.Text = pipeCanvas.GetType().GetField("showGrid", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(pipeCanvas) is true ? "Сетка: Вкл" : "Сетка: Выкл";
			};
			this.Controls.Add(this.btnResetView);
			this.Controls.Add(this.btnToggleGrid);
		}

		private System.Windows.Forms.ComboBox cmbPipeline;
		private System.Windows.Forms.ComboBox cmbInspection;
		private System.Windows.Forms.DataGridView dataGridViewDefects;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Button btnFilterCritical;
		private System.Windows.Forms.Button btnResetFilter;
		private System.Windows.Forms.Button btnRecalc;
		private System.Windows.Forms.Label lblPipeInfo;
		private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.Panel panelCanvas;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabTable;
		private System.Windows.Forms.TabPage tabCanvas;
		private System.Windows.Forms.Button btnResetView;
		private System.Windows.Forms.Button btnToggleGrid;
	}
}