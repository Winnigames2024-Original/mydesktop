namespace MyDesktop.UI
{
    partial class AccountWizard
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
            this.txtPass = new System.Windows.Forms.TextBox();
            this.lblPass2 = new System.Windows.Forms.Label();
            this.txtPass2 = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.lblTitle.Location = new System.Drawing.Point(26, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(220, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Создание аккаунта";
            //
            // lblSub
            //
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblSub.Location = new System.Drawing.Point(28, 60);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(250, 15);
            this.lblSub.TabIndex = 1;
            this.lblSub.Text = "Данные сохранятся на сервере (scrypt).";
            //
            // lblLogin
            //
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLogin.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblLogin.Location = new System.Drawing.Point(28, 96);
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
            this.txtLogin.Location = new System.Drawing.Point(28, 116);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(344, 27);
            this.txtLogin.TabIndex = 3;
            //
            // lblPass
            //
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPass.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblPass.Location = new System.Drawing.Point(28, 158);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(48, 15);
            this.lblPass.TabIndex = 4;
            this.lblPass.Text = "Пароль";
            //
            // txtPass
            //
            this.txtPass.BackColor = System.Drawing.Color.FromArgb(30, 36, 48);
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPass.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPass.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.txtPass.Location = new System.Drawing.Point(28, 178);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(344, 27);
            this.txtPass.TabIndex = 5;
            this.txtPass.UseSystemPasswordChar = true;
            //
            // lblPass2
            //
            this.lblPass2.AutoSize = true;
            this.lblPass2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPass2.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblPass2.Location = new System.Drawing.Point(28, 220);
            this.lblPass2.Name = "lblPass2";
            this.lblPass2.Size = new System.Drawing.Size(120, 15);
            this.lblPass2.TabIndex = 6;
            this.lblPass2.Text = "Повторите пароль";
            //
            // txtPass2
            //
            this.txtPass2.BackColor = System.Drawing.Color.FromArgb(30, 36, 48);
            this.txtPass2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPass2.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPass2.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.txtPass2.Location = new System.Drawing.Point(28, 240);
            this.txtPass2.Name = "txtPass2";
            this.txtPass2.Size = new System.Drawing.Size(344, 27);
            this.txtPass2.TabIndex = 7;
            this.txtPass2.UseSystemPasswordChar = true;
            //
            // btnCreate
            //
            this.btnCreate.BackColor = System.Drawing.Color.FromArgb(91, 140, 255);
            this.btnCreate.FlatAppearance.BorderSize = 0;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Location = new System.Drawing.Point(28, 288);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(344, 42);
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Text = "Создать";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            //
            // lblMsg
            //
            this.lblMsg.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMsg.ForeColor = System.Drawing.Color.FromArgb(255, 91, 110);
            this.lblMsg.Location = new System.Drawing.Point(26, 342);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(346, 56);
            this.lblMsg.TabIndex = 9;
            //
            // AccountWizard
            //
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(22, 26, 34);
            this.ClientSize = new System.Drawing.Size(400, 410);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtPass2);
            this.Controls.Add(this.lblPass2);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.lblSub);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MyDesktop — новый аккаунт";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label lblPass2;
        private System.Windows.Forms.TextBox txtPass2;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label lblMsg;
    }
}
