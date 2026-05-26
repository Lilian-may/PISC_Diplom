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

            // ========== MENU STRIP ==========
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(0, 70, 128);
            this.menuStrip.ForeColor = System.Drawing.Color.White;
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.fileMenu, this.referenceMenu, this.analysisMenu, this.reportsMenu, this.helpMenu});

            // Файл
            this.fileMenu.Text = "Файл";
            this.fileMenu.ForeColor = System.Drawing.Color.White;
            this.exitToolStripMenuItem.Text = "Выход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.exitToolStripMenuItem });

            // Справочники
            this.referenceMenu.Text = "Справочники";
            this.referenceMenu.ForeColor = System.Drawing.Color.White;
            this.pipelinesToolStripMenuItem.Text = "Трубопроводы";
            this.pipelinesToolStripMenuItem.Click += new System.EventHandler(this.pipelinesToolStripMenuItem_Click);
            this.inspectionsToolStripMenuItem.Text = "Инспекции";
            this.inspectionsToolStripMenuItem.Click += new System.EventHandler(this.inspectionsToolStripMenuItem_Click);
            this.referenceMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.pipelinesToolStripMenuItem, this.inspectionsToolStripMenuItem });

            // Анализ
            this.analysisMenu.Text = "Анализ";
            this.analysisMenu.ForeColor = System.Drawing.Color.White;
            this.defectsToolStripMenuItem.Text = "Дефекты";
            this.defectsToolStripMenuItem.Click += new System.EventHandler(this.defectsToolStripMenuItem_Click);
            this.analysisMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.defectsToolStripMenuItem });

            // Отчёты
            this.reportsMenu.Text = "Отчёты";
            this.reportsMenu.ForeColor = System.Drawing.Color.White;
            this.reportsToolStripMenuItem.Text = "Генерация отчётов";
            this.reportsToolStripMenuItem.Click += new System.EventHandler(this.reportsToolStripMenuItem_Click);
            this.reportsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.reportsToolStripMenuItem });

            // Справка
            this.helpMenu.Text = "Справка";
            this.helpMenu.ForeColor = System.Drawing.Color.White;
            this.aboutToolStripMenuItem.Text = "О программе";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.aboutToolStripMenuItem });

            // ========== STATUS STRIP ==========
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.statusStrip.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblUser.Text = "Пользователь:";
            this.lblDbStatus.Text = "Подключение к БД:";
            this.lblDbStatus.Spring = true;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.lblUser, this.lblDbStatus });

            // ========== MAIN FORM ==========
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Text = "ВТД Аналитика - ПАО «Газпром»";
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

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