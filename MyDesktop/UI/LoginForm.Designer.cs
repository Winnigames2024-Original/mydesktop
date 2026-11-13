namespace MyDesktop.UI
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lnkCreate = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.lblTitle.Location = new System.Drawing.Point(26, 28);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(230, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "С возвращением";
            //
            // lblSub
            //
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblSub.Location = new System.Drawing.Point(28, 66);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(180, 15);
            this.lblSub.TabIndex = 1;
            this.lblSub.Text = "Вход в аккаунт MyDesktop";
            //
            // lblLogin
            //
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLogin.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblLogin.Location = new System.Drawing.Point(28, 104);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(40, 15);
            this.lblLogin.TabIndex = 2;
            this.lblLogin.Text = "Логин";
            //
            // txtLogin
            //
            this.txtLogin.BackColor = System.Drawing.Color.FromArgb(30, 36, 48);
            this.txtLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogin.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtLogin.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.txtLogin.Location = new System.Drawing.Point(28, 124);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(344, 27);
            this.txtLogin.TabIndex = 3;
            //
            // lblPass
            //
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPass.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblPass.Location = new System.Drawing.Point(28, 166);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(48, 15);
            this.lblPass.TabIndex = 4;
            this.lblPass.Text = "Пароль";
            //
            // txtPassword
            //
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(30, 36, 48);
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.txtPassword.Location = new System.Drawing.Point(28, 186);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(344, 27);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            //
            // btnLogin
            //
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(91, 140, 255);
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(28, 234);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(344, 42);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "Войти";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            //
            // lblMsg
            //
            this.lblMsg.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMsg.ForeColor = System.Drawing.Color.FromArgb(255, 91, 110);
            this.lblMsg.Location = new System.Drawing.Point(26, 290);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(346, 44);
            this.lblMsg.TabIndex = 7;
            //
            // lnkCreate
            //
            this.lnkCreate.ActiveLinkColor = System.Drawing.Color.FromArgb(124, 92, 255);
            this.lnkCreate.AutoSize = true;
            this.lnkCreate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lnkCreate.LinkColor = System.Drawing.Color.FromArgb(91, 140, 255);
            this.lnkCreate.Location = new System.Drawing.Point(28, 346);
            this.lnkCreate.Name = "lnkCreate";
            this.lnkCreate.Size = new System.Drawing.Size(146, 15);
            this.lnkCreate.TabIndex = 8;
            this.lnkCreate.TabStop = true;
            this.lnkCreate.Text = "Создать новый аккаунт";
            this.lnkCreate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCreate_LinkClicked);
            //
            // LoginForm
            //
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(22, 26, 34);
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this.lnkCreate);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.lblSub);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyDesktop — вход";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.LinkLabel lnkCreate;
    }
}
