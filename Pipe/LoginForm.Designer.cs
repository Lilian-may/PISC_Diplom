namespace Pipe
{
    partial class LoginForm
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
            this.headerPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();

            this.contentPanel = new System.Windows.Forms.Panel();
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCreateFirstUser = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();

            this.footerPanel = new System.Windows.Forms.Panel();
            this.lblVersion = new System.Windows.Forms.Label();

            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.SuspendLayout();

            // ========== HEADER PANEL ==========
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(0, 70, 128);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Height = 100;
            this.headerPanel.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);

            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(30, 25);
            this.titleLabel.Text = "ВТД Аналитика";

            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.subtitleLabel.Location = new System.Drawing.Point(32, 58);
            this.subtitleLabel.Text = "Система анализа данных внутритрубной диагностики";

            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.subtitleLabel);

            // ========== CONTENT PANEL ==========
            this.contentPanel.BackColor = System.Drawing.Color.White;
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Padding = new System.Windows.Forms.Padding(40, 30, 40, 30);

            // Логин
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblLogin.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblLogin.Location = new System.Drawing.Point(40, 35);
            this.lblLogin.Text = "Логин";

            this.txtLogin.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtLogin.Location = new System.Drawing.Point(40, 60);
            this.txtLogin.Size = new System.Drawing.Size(320, 27);
            this.txtLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Пароль
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblPassword.Location = new System.Drawing.Point(40, 105);
            this.lblPassword.Text = "Пароль";

            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPassword.Location = new System.Drawing.Point(40, 130);
            this.txtPassword.Size = new System.Drawing.Size(320, 27);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);

            // Кнопка входа
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogin.Text = "Войти";
            this.btnLogin.Size = new System.Drawing.Size(150, 45);
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Location = new System.Drawing.Point(40, 180);
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // Кнопка создания первого пользователя
            this.btnCreateFirstUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateFirstUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnCreateFirstUser.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.btnCreateFirstUser.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.btnCreateFirstUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCreateFirstUser.Text = "Создать первого пользователя";
            this.btnCreateFirstUser.Size = new System.Drawing.Size(210, 45);
            this.btnCreateFirstUser.FlatAppearance.BorderSize = 1;
            this.btnCreateFirstUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateFirstUser.Location = new System.Drawing.Point(210, 180);
            this.btnCreateFirstUser.Visible = false;
            this.btnCreateFirstUser.Click += new System.EventHandler(this.btnCreateFirstUser_Click);

            // Информационная метка
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            this.lblInfo.Location = new System.Drawing.Point(40, 245);
            this.lblInfo.Text = "Введите логин и пароль для входа";

            this.contentPanel.Controls.Add(this.lblLogin);
            this.contentPanel.Controls.Add(this.txtLogin);
            this.contentPanel.Controls.Add(this.lblPassword);
            this.contentPanel.Controls.Add(this.txtPassword);
            this.contentPanel.Controls.Add(this.btnLogin);
            this.contentPanel.Controls.Add(this.btnCreateFirstUser);
            this.contentPanel.Controls.Add(this.lblInfo);

            // ========== FOOTER PANEL ==========
            this.footerPanel.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Height = 35;
            this.footerPanel.Padding = new System.Windows.Forms.Padding(30, 8, 30, 8);

            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            this.lblVersion.Location = new System.Drawing.Point(30, 10);
            this.lblVersion.Text = "ПАО «Газпром» | Версия 2.0 | Соответствует СТО Газпром 2-2.3-112-2007";

            this.footerPanel.Controls.Add(this.lblVersion);

            // ========== MAIN FORM ==========
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 380);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.footerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Авторизация - ВТД Аналитика";
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.headerPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCreateFirstUser;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Label lblVersion;
    }
}