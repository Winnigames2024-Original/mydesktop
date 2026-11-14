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
            this.lblShareHdr = new System.Windows.Forms.Label();
            this.lblShareKey = new System.Windows.Forms.Label();
            this.txtShareKey = new System.Windows.Forms.TextBox();
            this.btnShare = new System.Windows.Forms.Button();
            this.lblShareStatus = new System.Windows.Forms.Label();
            this.lblWatchCap = new System.Windows.Forms.Label();
            this.txtWatchUrl = new System.Windows.Forms.TextBox();
            this.lblRdHdr = new System.Windows.Forms.Label();
            this.lblRelay = new System.Windows.Forms.Label();
            this.btnOpenRustdesk = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblRdStatus = new System.Windows.Forms.Label();
            this.lblOpHdr = new System.Windows.Forms.Label();
            this.lblPeerKey = new System.Windows.Forms.Label();
            this.txtPeerKey = new System.Windows.Forms.TextBox();
            this.btnWatch = new System.Windows.Forms.Button();
            this.lblPeerId = new System.Windows.Forms.Label();
            this.txtPeerId = new System.Windows.Forms.TextBox();
            this.btnControl = new System.Windows.Forms.Button();
            this.lblNote = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // lblShareHdr
            //
            this.lblShareHdr.AutoSize = true;
            this.lblShareHdr.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblShareHdr.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.lblShareHdr.Location = new System.Drawing.Point(20, 18);
            this.lblShareHdr.Name = "lblShareHdr";
            this.lblShareHdr.Size = new System.Drawing.Size(260, 25);
            this.lblShareHdr.TabIndex = 0;
            this.lblShareHdr.Text = "Показать свой экран (как OBS)";
            //
            // lblShareKey
            //
            this.lblShareKey.AutoSize = true;
            this.lblShareKey.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblShareKey.Location = new System.Drawing.Point(22, 54);
            this.lblShareKey.Name = "lblShareKey";
            this.lblShareKey.Size = new System.Drawing.Size(74, 15);
            this.lblShareKey.Text = "Ключ сессии";
            //
            // txtShareKey
            //
            this.txtShareKey.BackColor = System.Drawing.Color.FromArgb(30, 36, 48);
            this.txtShareKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShareKey.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.txtShareKey.Location = new System.Drawing.Point(24, 74);
            this.txtShareKey.Name = "txtShareKey";
            this.txtShareKey.Size = new System.Drawing.Size(250, 23);
            this.txtShareKey.TabIndex = 1;
            this.txtShareKey.TextChanged += new System.EventHandler(this.txtShareKey_TextChanged);
            //
            // btnShare
            //
            this.btnShare.BackColor = System.Drawing.Color.FromArgb(91, 140, 255);
            this.btnShare.FlatAppearance.BorderSize = 0;
            this.btnShare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShare.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnShare.ForeColor = System.Drawing.Color.White;
            this.btnShare.Location = new System.Drawing.Point(24, 108);
            this.btnShare.Name = "btnShare";
            this.btnShare.Size = new System.Drawing.Size(250, 40);
            this.btnShare.TabIndex = 2;
            this.btnShare.Text = "Показать экран оператору";
            this.btnShare.UseVisualStyleBackColor = false;
            this.btnShare.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShare.Click += new System.EventHandler(this.btnShare_Click);
            //
            // lblShareStatus
            //
            this.lblShareStatus.AutoSize = true;
            this.lblShareStatus.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblShareStatus.Location = new System.Drawing.Point(22, 156);
            this.lblShareStatus.Name = "lblShareStatus";
            this.lblShareStatus.Size = new System.Drawing.Size(66, 15);
            this.lblShareStatus.Text = "● выключено";
            //
            // lblWatchCap
            //
            this.lblWatchCap.AutoSize = true;
            this.lblWatchCap.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblWatchCap.Location = new System.Drawing.Point(22, 182);
            this.lblWatchCap.Name = "lblWatchCap";
            this.lblWatchCap.Size = new System.Drawing.Size(140, 15);
            this.lblWatchCap.Text = "Оператор смотрит (HLS):";
            //
            // txtWatchUrl
            //
            this.txtWatchUrl.BackColor = System.Drawing.Color.FromArgb(30, 36, 48);
            this.txtWatchUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWatchUrl.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.txtWatchUrl.Location = new System.Drawing.Point(24, 202);
            this.txtWatchUrl.Name = "txtWatchUrl";
            this.txtWatchUrl.ReadOnly = true;
            this.txtWatchUrl.Size = new System.Drawing.Size(340, 23);
            this.txtWatchUrl.TabIndex = 3;
            //
            // lblRdHdr
            //
            this.lblRdHdr.AutoSize = true;
            this.lblRdHdr.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblRdHdr.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.lblRdHdr.Location = new System.Drawing.Point(20, 250);
            this.lblRdHdr.Name = "lblRdHdr";
            this.lblRdHdr.Size = new System.Drawing.Size(250, 25);
            this.lblRdHdr.TabIndex = 4;
            this.lblRdHdr.Text = "Управление через RustDesk";
            //
            // lblRelay
            //
            this.lblRelay.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblRelay.Location = new System.Drawing.Point(22, 282);
            this.lblRelay.Name = "lblRelay";
            this.lblRelay.Size = new System.Drawing.Size(360, 40);
            this.lblRelay.Text = "Релей: —";
            //
            // btnOpenRustdesk
            //
            this.btnOpenRustdesk.BackColor = System.Drawing.Color.FromArgb(91, 140, 255);
            this.btnOpenRustdesk.FlatAppearance.BorderSize = 0;
            this.btnOpenRustdesk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenRustdesk.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnOpenRustdesk.ForeColor = System.Drawing.Color.White;
            this.btnOpenRustdesk.Location = new System.Drawing.Point(24, 328);
            this.btnOpenRustdesk.Name = "btnOpenRustdesk";
            this.btnOpenRustdesk.Size = new System.Drawing.Size(250, 40);
            this.btnOpenRustdesk.TabIndex = 5;
            this.btnOpenRustdesk.Text = "Настроить и открыть RustDesk";
            this.btnOpenRustdesk.UseVisualStyleBackColor = false;
            this.btnOpenRustdesk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenRustdesk.Click += new System.EventHandler(this.btnOpenRustdesk_Click);
            //
            // btnDownload
            //
            this.btnDownload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(38, 45, 58);
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.btnDownload.Location = new System.Drawing.Point(284, 328);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(160, 40);
            this.btnDownload.TabIndex = 6;
            this.btnDownload.Text = "Скачать RustDesk";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            //
            // lblRdStatus
            //
            this.lblRdStatus.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblRdStatus.Location = new System.Drawing.Point(22, 376);
            this.lblRdStatus.Name = "lblRdStatus";
            this.lblRdStatus.Size = new System.Drawing.Size(420, 40);
            this.lblRdStatus.Text = "";
            //
            // lblOpHdr
            //
            this.lblOpHdr.AutoSize = true;
            this.lblOpHdr.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblOpHdr.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.lblOpHdr.Location = new System.Drawing.Point(430, 18);
            this.lblOpHdr.Name = "lblOpHdr";
            this.lblOpHdr.Size = new System.Drawing.Size(230, 25);
            this.lblOpHdr.TabIndex = 7;
            this.lblOpHdr.Text = "Подключиться к другому";
            //
            // lblPeerKey
            //
            this.lblPeerKey.AutoSize = true;
            this.lblPeerKey.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblPeerKey.Location = new System.Drawing.Point(432, 54);
            this.lblPeerKey.Name = "lblPeerKey";
            this.lblPeerKey.Size = new System.Drawing.Size(168, 15);
            this.lblPeerKey.Text = "Ключ сессии для просмотра";
            //
            // txtPeerKey
            //
            this.txtPeerKey.BackColor = System.Drawing.Color.FromArgb(30, 36, 48);
            this.txtPeerKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPeerKey.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.txtPeerKey.Location = new System.Drawing.Point(434, 74);
            this.txtPeerKey.Name = "txtPeerKey";
            this.txtPeerKey.Size = new System.Drawing.Size(240, 23);
            this.txtPeerKey.TabIndex = 8;
            //
            // btnWatch
            //
            this.btnWatch.BackColor = System.Drawing.Color.FromArgb(91, 140, 255);
            this.btnWatch.FlatAppearance.BorderSize = 0;
            this.btnWatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWatch.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnWatch.ForeColor = System.Drawing.Color.White;
            this.btnWatch.Location = new System.Drawing.Point(434, 106);
            this.btnWatch.Name = "btnWatch";
            this.btnWatch.Size = new System.Drawing.Size(240, 36);
            this.btnWatch.TabIndex = 9;
            this.btnWatch.Text = "Смотреть экран (HLS)";
            this.btnWatch.UseVisualStyleBackColor = false;
            this.btnWatch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWatch.Click += new System.EventHandler(this.btnWatch_Click);
            //
            // lblPeerId
            //
            this.lblPeerId.AutoSize = true;
            this.lblPeerId.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblPeerId.Location = new System.Drawing.Point(432, 166);
            this.lblPeerId.Name = "lblPeerId";
            this.lblPeerId.Size = new System.Drawing.Size(150, 15);
            this.lblPeerId.Text = "RustDesk ID устройства";
            //
            // txtPeerId
            //
            this.txtPeerId.BackColor = System.Drawing.Color.FromArgb(30, 36, 48);
            this.txtPeerId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPeerId.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.txtPeerId.Location = new System.Drawing.Point(434, 186);
            this.txtPeerId.Name = "txtPeerId";
            this.txtPeerId.Size = new System.Drawing.Size(240, 23);
            this.txtPeerId.TabIndex = 10;
            //
            // btnControl
            //
            this.btnControl.BackColor = System.Drawing.Color.FromArgb(124, 92, 255);
            this.btnControl.FlatAppearance.BorderSize = 0;
            this.btnControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnControl.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnControl.ForeColor = System.Drawing.Color.White;
            this.btnControl.Location = new System.Drawing.Point(434, 218);
            this.btnControl.Name = "btnControl";
            this.btnControl.Size = new System.Drawing.Size(240, 36);
            this.btnControl.TabIndex = 11;
            this.btnControl.Text = "Управлять (RustDesk)";
            this.btnControl.UseVisualStyleBackColor = false;
            this.btnControl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnControl.Click += new System.EventHandler(this.btnControl_Click);
            //
            // lblNote
            //
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblNote.Location = new System.Drawing.Point(432, 274);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(300, 80);
            this.lblNote.Text = "Экран передаётся потоком (как OBS) и только по согласию — пока идёт показ, на управляемой машине виден индикатор. Управление мышью/клавиатурой — через RustDesk.";
            //
            // RemotePanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(13, 15, 20);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.btnControl);
            this.Controls.Add(this.txtPeerId);
            this.Controls.Add(this.lblPeerId);
            this.Controls.Add(this.btnWatch);
            this.Controls.Add(this.txtPeerKey);
            this.Controls.Add(this.lblPeerKey);
            this.Controls.Add(this.lblOpHdr);
            this.Controls.Add(this.lblRdStatus);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnOpenRustdesk);
            this.Controls.Add(this.lblRelay);
            this.Controls.Add(this.lblRdHdr);
            this.Controls.Add(this.txtWatchUrl);
            this.Controls.Add(this.lblWatchCap);
            this.Controls.Add(this.lblShareStatus);
            this.Controls.Add(this.btnShare);
            this.Controls.Add(this.txtShareKey);
            this.Controls.Add(this.lblShareKey);
            this.Controls.Add(this.lblShareHdr);
            this.Name = "RemotePanel";
            this.Size = new System.Drawing.Size(760, 540);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblShareHdr;
        private System.Windows.Forms.Label lblShareKey;
        private System.Windows.Forms.TextBox txtShareKey;
        private System.Windows.Forms.Button btnShare;
        private System.Windows.Forms.Label lblShareStatus;
        private System.Windows.Forms.Label lblWatchCap;
        private System.Windows.Forms.TextBox txtWatchUrl;
        private System.Windows.Forms.Label lblRdHdr;
        private System.Windows.Forms.Label lblRelay;
        private System.Windows.Forms.Button btnOpenRustdesk;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label lblRdStatus;
        private System.Windows.Forms.Label lblOpHdr;
        private System.Windows.Forms.Label lblPeerKey;
        private System.Windows.Forms.TextBox txtPeerKey;
        private System.Windows.Forms.Button btnWatch;
        private System.Windows.Forms.Label lblPeerId;
        private System.Windows.Forms.TextBox txtPeerId;
        private System.Windows.Forms.Button btnControl;
        private System.Windows.Forms.Label lblNote;
    }
}
