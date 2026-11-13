namespace MyDesktop.UI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlSide = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.btnAccount = new System.Windows.Forms.Button();
            this.btnRemote = new System.Windows.Forms.Button();
            this.btnEffects = new System.Windows.Forms.Button();
            this.btnStream = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblContentText = new System.Windows.Forms.Label();
            this.lblContentTitle = new System.Windows.Forms.Label();
            this.pnlSide.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlSide
            //
            this.pnlSide.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlSide.BackColor = System.Drawing.Color.FromArgb(22, 26, 34);
            this.pnlSide.Controls.Add(this.btnLogout);
            this.pnlSide.Controls.Add(this.lblUser);
            this.pnlSide.Controls.Add(this.btnAccount);
            this.pnlSide.Controls.Add(this.btnRemote);
            this.pnlSide.Controls.Add(this.btnEffects);
            this.pnlSide.Controls.Add(this.btnStream);
            this.pnlSide.Controls.Add(this.btnHome);
            this.pnlSide.Location = new System.Drawing.Point(0, 0);
            this.pnlSide.Name = "pnlSide";
            this.pnlSide.Size = new System.Drawing.Size(210, 640);
            this.pnlSide.TabIndex = 0;
            //
            // btnLogout
            //
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLogout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(38, 45, 58);
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(255, 91, 110);
            this.btnLogout.Location = new System.Drawing.Point(12, 548);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(186, 36);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "Выйти";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            //
            // lblUser
            //
            this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(61, 220, 132);
            this.lblUser.Location = new System.Drawing.Point(16, 520);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(20, 15);
            this.lblUser.TabIndex = 5;
            this.lblUser.Text = "●";
            //
            // btnAccount
            //
            this.btnAccount.FlatAppearance.BorderSize = 0;
            this.btnAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAccount.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.btnAccount.Location = new System.Drawing.Point(12, 208);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(186, 44);
            this.btnAccount.TabIndex = 4;
            this.btnAccount.Tag = "Аккаунт";
            this.btnAccount.Text = "   Аккаунт";
            this.btnAccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAccount.UseVisualStyleBackColor = true;
            this.btnAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAccount.Click += new System.EventHandler(this.btnNav_Click);
            //
            // btnRemote
            //
            this.btnRemote.FlatAppearance.BorderSize = 0;
            this.btnRemote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemote.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRemote.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.btnRemote.Location = new System.Drawing.Point(12, 160);
            this.btnRemote.Name = "btnRemote";
            this.btnRemote.Size = new System.Drawing.Size(186, 44);
            this.btnRemote.TabIndex = 3;
            this.btnRemote.Tag = "Удалённый доступ";
            this.btnRemote.Text = "   Удалённый доступ";
            this.btnRemote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemote.UseVisualStyleBackColor = true;
            this.btnRemote.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemote.Click += new System.EventHandler(this.btnNav_Click);
            //
            // btnEffects
            //
            this.btnEffects.FlatAppearance.BorderSize = 0;
            this.btnEffects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEffects.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnEffects.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.btnEffects.Location = new System.Drawing.Point(12, 112);
            this.btnEffects.Name = "btnEffects";
            this.btnEffects.Size = new System.Drawing.Size(186, 44);
            this.btnEffects.TabIndex = 2;
            this.btnEffects.Tag = "Эффекты";
            this.btnEffects.Text = "   Эффекты";
            this.btnEffects.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEffects.UseVisualStyleBackColor = true;
            this.btnEffects.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEffects.Click += new System.EventHandler(this.btnNav_Click);
            //
            // btnStream
            //
            this.btnStream.FlatAppearance.BorderSize = 0;
            this.btnStream.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStream.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnStream.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.btnStream.Location = new System.Drawing.Point(12, 64);
            this.btnStream.Name = "btnStream";
            this.btnStream.Size = new System.Drawing.Size(186, 44);
            this.btnStream.TabIndex = 1;
            this.btnStream.Tag = "Трансляция";
            this.btnStream.Text = "   Трансляция";
            this.btnStream.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStream.UseVisualStyleBackColor = true;
            this.btnStream.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStream.Click += new System.EventHandler(this.btnNav_Click);
            //
            // btnHome
            //
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnHome.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.btnHome.Location = new System.Drawing.Point(12, 16);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(186, 44);
            this.btnHome.TabIndex = 0;
            this.btnHome.Tag = "Главная";
            this.btnHome.Text = "   Главная";
            this.btnHome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHome.Click += new System.EventHandler(this.btnNav_Click);
            //
            // pnlContent
            //
            this.pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(13, 15, 20);
            this.pnlContent.Controls.Add(this.lblStatus);
            this.pnlContent.Controls.Add(this.lblContentText);
            this.pnlContent.Controls.Add(this.lblContentTitle);
            this.pnlContent.Location = new System.Drawing.Point(210, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(790, 640);
            this.pnlContent.TabIndex = 1;
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblStatus.Location = new System.Drawing.Point(26, 120);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(120, 15);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Проверяю сервер…";
            //
            // lblContentText
            //
            this.lblContentText.AutoSize = true;
            this.lblContentText.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblContentText.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblContentText.Location = new System.Drawing.Point(26, 76);
            this.lblContentText.MaximumSize = new System.Drawing.Size(720, 0);
            this.lblContentText.Name = "lblContentText";
            this.lblContentText.Size = new System.Drawing.Size(0, 15);
            this.lblContentText.TabIndex = 1;
            //
            // lblContentTitle
            //
            this.lblContentTitle.AutoSize = true;
            this.lblContentTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblContentTitle.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.lblContentTitle.Location = new System.Drawing.Point(24, 28);
            this.lblContentTitle.Name = "lblContentTitle";
            this.lblContentTitle.Size = new System.Drawing.Size(100, 32);
            this.lblContentTitle.TabIndex = 0;
            this.lblContentTitle.Text = "Главная";
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(13, 15, 20);
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSide);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.MinimumSize = new System.Drawing.Size(880, 560);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyDesktop";
            this.pnlSide.ResumeLayout(false);
            this.pnlSide.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlSide;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnStream;
        private System.Windows.Forms.Button btnEffects;
        private System.Windows.Forms.Button btnRemote;
        private System.Windows.Forms.Button btnAccount;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblContentTitle;
        private System.Windows.Forms.Label lblContentText;
        private System.Windows.Forms.Label lblStatus;
    }
}
