namespace MyDesktop.UI.Panels
{
    partial class RemotePanel
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblHdr = new System.Windows.Forms.Label();
            this.lblMyCap = new System.Windows.Forms.Label();
            this.lblMyLogin = new System.Windows.Forms.Label();
            this.btnAllow = new System.Windows.Forms.Button();
            this.lblAllowStatus = new System.Windows.Forms.Label();
            this.lblConnHdr = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblConnStatus = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // lblHdr
            //
            this.lblHdr.AutoSize = true;
            this.lblHdr.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblHdr.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.lblHdr.Location = new System.Drawing.Point(20, 18);
            this.lblHdr.Name = "lblHdr";
            this.lblHdr.Size = new System.Drawing.Size(220, 32);
            this.lblHdr.TabIndex = 0;
            this.lblHdr.Text = "Удалённый доступ";
            //
            // lblMyCap
            //
            this.lblMyCap.AutoSize = true;
            this.lblMyCap.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblMyCap.Location = new System.Drawing.Point(22, 66);
            this.lblMyCap.Name = "lblMyCap";
            this.lblMyCap.Size = new System.Drawing.Size(70, 15);
            this.lblMyCap.Text = "Ваш логин:";
            //
            // lblMyLogin
            //
            this.lblMyLogin.AutoSize = true;
            this.lblMyLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblMyLogin.ForeColor = System.Drawing.Color.FromArgb(61, 220, 132);
            this.lblMyLogin.Location = new System.Drawing.Point(98, 65);
            this.lblMyLogin.Name = "lblMyLogin";
            this.lblMyLogin.Size = new System.Drawing.Size(10, 19);
            this.lblMyLogin.Text = "—";
            //
            // btnAllow
            //
            this.btnAllow.BackColor = System.Drawing.Color.FromArgb(91, 140, 255);
            this.btnAllow.FlatAppearance.BorderSize = 0;
            this.btnAllow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllow.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAllow.ForeColor = System.Drawing.Color.White;
            this.btnAllow.Location = new System.Drawing.Point(24, 96);
            this.btnAllow.Name = "btnAllow";
            this.btnAllow.Size = new System.Drawing.Size(300, 42);
            this.btnAllow.TabIndex = 1;
            this.btnAllow.Text = "Разрешить просмотр моего экрана";
            this.btnAllow.UseVisualStyleBackColor = false;
            this.btnAllow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAllow.Click += new System.EventHandler(this.btnAllow_Click);
            //
            // lblAllowStatus
            //
            this.lblAllowStatus.AutoSize = true;
            this.lblAllowStatus.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblAllowStatus.Location = new System.Drawing.Point(22, 146);
            this.lblAllowStatus.Name = "lblAllowStatus";
            this.lblAllowStatus.Size = new System.Drawing.Size(66, 15);
            this.lblAllowStatus.Text = "● выключено";
            //
            // lblConnHdr
            //
            this.lblConnHdr.AutoSize = true;
            this.lblConnHdr.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblConnHdr.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.lblConnHdr.Location = new System.Drawing.Point(20, 196);
            this.lblConnHdr.Name = "lblConnHdr";
            this.lblConnHdr.Size = new System.Drawing.Size(270, 25);
            this.lblConnHdr.TabIndex = 2;
            this.lblConnHdr.Text = "Подключиться к компьютеру";
            //
            // lblLogin
            //
            this.lblLogin.AutoSize = true;
            this.lblLogin.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblLogin.Location = new System.Drawing.Point(22, 232);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(40, 15);
            this.lblLogin.Text = "Логин";
            //
            // txtLogin
            //
            this.txtLogin.BackColor = System.Drawing.Color.FromArgb(30, 36, 48);
            this.txtLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogin.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.txtLogin.Location = new System.Drawing.Point(24, 252);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(300, 23);
            this.txtLogin.TabIndex = 3;
            //
            // lblPass
            //
            this.lblPass.AutoSize = true;
            this.lblPass.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblPass.Location = new System.Drawing.Point(22, 288);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(48, 15);
            this.lblPass.Text = "Пароль";
            //
            // txtPass
            //
            this.txtPass.BackColor = System.Drawing.Color.FromArgb(30, 36, 48);
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPass.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.txtPass.Location = new System.Drawing.Point(24, 308);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(300, 23);
            this.txtPass.TabIndex = 4;
            this.txtPass.UseSystemPasswordChar = true;
            //
            // btnConnect
            //
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(91, 140, 255);
            this.btnConnect.FlatAppearance.BorderSize = 0;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Location = new System.Drawing.Point(24, 346);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(300, 42);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Подключиться";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            //
            // lblConnStatus
            //
            this.lblConnStatus.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblConnStatus.Location = new System.Drawing.Point(22, 396);
            this.lblConnStatus.Name = "lblConnStatus";
            this.lblConnStatus.Size = new System.Drawing.Size(460, 40);
            this.lblConnStatus.Text = "";
            //
            // lblNote
            //
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblNote.Location = new System.Drawing.Point(22, 448);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(500, 60);
            this.lblNote.Text = "Подключение по логину и паролю аккаунта. Пока идёт показ экрана, на управляемой машине виден индикатор сессии.";
            //
            // RemotePanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(13, 15, 20);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.lblConnStatus);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.lblConnHdr);
            this.Controls.Add(this.lblAllowStatus);
            this.Controls.Add(this.btnAllow);
            this.Controls.Add(this.lblMyLogin);
            this.Controls.Add(this.lblMyCap);
            this.Controls.Add(this.lblHdr);
            this.Name = "RemotePanel";
            this.Size = new System.Drawing.Size(760, 540);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblHdr;
        private System.Windows.Forms.Label lblMyCap;
        private System.Windows.Forms.Label lblMyLogin;
        private System.Windows.Forms.Button btnAllow;
        private System.Windows.Forms.Label lblAllowStatus;
        private System.Windows.Forms.Label lblConnHdr;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblConnStatus;
        private System.Windows.Forms.Label lblNote;
    }
}
