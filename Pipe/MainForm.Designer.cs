namespace Pipe
{
    partial class MainForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.referenceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.pipelinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inspectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analysisMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.defectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // menuStrip
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.fileMenu, this.referenceMenu, this.analysisMenu, this.reportsMenu, this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(900, 24);
            this.menuStrip.TabIndex = 0;

            // fileMenu
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.exitToolStripMenuItem });
            this.fileMenu.Text = "Файл";
            // exitToolStripMenuItem
            this.exitToolStripMenuItem.Text = "Выход";
            this.exitToolStripMenuItem.Click += this.exitToolStripMenuItem_Click;

            // referenceMenu
            this.referenceMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.pipelinesToolStripMenuItem, this.inspectionsToolStripMenuItem });
            this.referenceMenu.Text = "Справочники";
            // pipelinesToolStripMenuItem
            this.pipelinesToolStripMenuItem.Text = "Трубопроводы";
            this.pipelinesToolStripMenuItem.Click += this.pipelinesToolStripMenuItem_Click;
            // inspectionsToolStripMenuItem
            this.inspectionsToolStripMenuItem.Text = "Инспекции";
            this.inspectionsToolStripMenuItem.Click += this.inspectionsToolStripMenuItem_Click;

            // analysisMenu
            this.analysisMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.defectsToolStripMenuItem });
            this.analysisMenu.Text = "Анализ";
            // defectsToolStripMenuItem
            this.defectsToolStripMenuItem.Text = "Дефекты";
            this.defectsToolStripMenuItem.Click += this.defectsToolStripMenuItem_Click;

            // reportsMenu
            this.reportsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.reportsToolStripMenuItem });
            this.reportsMenu.Text = "Отчёты";
            // reportsToolStripMenuItem
            this.reportsToolStripMenuItem.Text = "Генерация отчётов";
            this.reportsToolStripMenuItem.Click += this.reportsToolStripMenuItem_Click;

            // helpMenu
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.aboutToolStripMenuItem });
            this.helpMenu.Text = "Справка";
            // aboutToolStripMenuItem
            this.aboutToolStripMenuItem.Text = "О программе";
            this.aboutToolStripMenuItem.Click += this.aboutToolStripMenuItem_Click;

            // statusStrip
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.lblUser, this.lblDbStatus });
            this.statusStrip.Location = new System.Drawing.Point(0, 528);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(900, 22);
            // lblUser
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(100, 17);
            this.lblUser.Text = "Пользователь:";
            // lblDbStatus
            this.lblDbStatus.Name = "lblDbStatus";
            this.lblDbStatus.Size = new System.Drawing.Size(120, 17);
            this.lblDbStatus.Text = "Подключение к БД:";

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Text = "ВТД Аналитика - ПАО «Газпром»";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem referenceMenu;
        private System.Windows.Forms.ToolStripMenuItem pipelinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inspectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analysisMenu;
        private System.Windows.Forms.ToolStripMenuItem defectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsMenu;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblUser;
        private System.Windows.Forms.ToolStripStatusLabel lblDbStatus;
    }
}