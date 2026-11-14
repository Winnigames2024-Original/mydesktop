namespace MyDesktop.UI.Panels
{
    partial class StreamPanel
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
            this.lblMon = new System.Windows.Forms.Label();
            this.cmbMonitor = new System.Windows.Forms.ComboBox();
            this.lblFps = new System.Windows.Forms.Label();
            this.numFps = new System.Windows.Forms.NumericUpDown();
            this.lblBr = new System.Windows.Forms.Label();
            this.numBitrate = new System.Windows.Forms.NumericUpDown();
            this.lblKey = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.lblFf = new System.Windows.Forms.Label();
            this.txtFfmpeg = new System.Windows.Forms.TextBox();
            this.lblUrlCap = new System.Windows.Forms.Label();
            this.lblUrl = new System.Windows.Forms.Label();
            this.btnToggle = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblHlsCap = new System.Windows.Forms.Label();
            this.txtHls = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numFps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBitrate)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(290, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Трансляция «как OBS»";
            //
            // lblMon
            //
            this.lblMon.AutoSize = true;
            this.lblMon.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblMon.Location = new System.Drawing.Point(22, 66);
            this.lblMon.Name = "lblMon";
            this.lblMon.Size = new System.Drawing.Size(56, 15);
            this.lblMon.TabIndex = 1;
            this.lblMon.Text = "Монитор";
            //
            // cmbMonitor
            //
            this.cmbMonitor.BackColor = System.Drawing.Color.FromArgb(30, 36, 48);
            this.cmbMonitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonitor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMonitor.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.cmbMonitor.Location = new System.Drawing.Point(24, 86);
            this.cmbMonitor.Name = "cmbMonitor";
            this.cmbMonitor.Size = new System.Drawing.Size(320, 24);
            this.cmbMonitor.TabIndex = 2;
            //
            // lblFps
            //
            this.lblFps.AutoSize = true;
            this.lblFps.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblFps.Location = new System.Drawing.Point(362, 66);
            this.lblFps.Name = "lblFps";
            this.lblFps.Size = new System.Drawing.Size(28, 15);
            this.lblFps.TabIndex = 3;
            this.lblFps.Text = "FPS";
            //
            // numFps
            //
            this.numFps.BackColor = System.Drawing.Color.FromArgb(30, 36, 48);
            this.numFps.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.numFps.Location = new System.Drawing.Point(364, 86);
            this.numFps.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numFps.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            this.numFps.Value = new decimal(new int[] { 30, 0, 0, 0 });
            this.numFps.Name = "numFps";
            this.numFps.Size = new System.Drawing.Size(90, 23);
            this.numFps.TabIndex = 4;
            //
            // lblBr
            //
            this.lblBr.AutoSize = true;
            this.lblBr.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblBr.Location = new System.Drawing.Point(474, 66);
            this.lblBr.Name = "lblBr";
            this.lblBr.Size = new System.Drawing.Size(92, 15);
            this.lblBr.TabIndex = 5;
            this.lblBr.Text = "Битрейт, kbps";
            //
            // numBitrate
            //
            this.numBitrate.BackColor = System.Drawing.Color.FromArgb(30, 36, 48);
            this.numBitrate.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.numBitrate.Location = new System.Drawing.Point(476, 86);
            this.numBitrate.Minimum = new decimal(new int[] { 500, 0, 0, 0 });
            this.numBitrate.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            this.numBitrate.Increment = new decimal(new int[] { 500, 0, 0, 0 });
            this.numBitrate.Value = new decimal(new int[] { 2500, 0, 0, 0 });
            this.numBitrate.Name = "numBitrate";
            this.numBitrate.Size = new System.Drawing.Size(140, 23);
            this.numBitrate.TabIndex = 6;
            //
            // lblKey
            //
            this.lblKey.AutoSize = true;
            this.lblKey.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblKey.Location = new System.Drawing.Point(22, 128);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(104, 15);
            this.lblKey.TabIndex = 7;
            this.lblKey.Text = "Ключ трансляции";
            //
            // txtKey
            //
            this.txtKey.BackColor = System.Drawing.Color.FromArgb(30, 36, 48);
            this.txtKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKey.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.txtKey.Location = new System.Drawing.Point(24, 148);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(320, 23);
            this.txtKey.TabIndex = 8;
            this.txtKey.TextChanged += new System.EventHandler(this.txtKey_TextChanged);
            //
            // lblFf
            //
            this.lblFf.AutoSize = true;
            this.lblFf.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblFf.Location = new System.Drawing.Point(362, 128);
            this.lblFf.Name = "lblFf";
            this.lblFf.Size = new System.Drawing.Size(150, 15);
            this.lblFf.TabIndex = 9;
            this.lblFf.Text = "ffmpeg (путь или 'ffmpeg')";
            //
            // txtFfmpeg
            //
            this.txtFfmpeg.BackColor = System.Drawing.Color.FromArgb(30, 36, 48);
            this.txtFfmpeg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFfmpeg.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.txtFfmpeg.Location = new System.Drawing.Point(364, 148);
            this.txtFfmpeg.Name = "txtFfmpeg";
            this.txtFfmpeg.Size = new System.Drawing.Size(340, 23);
            this.txtFfmpeg.TabIndex = 10;
            //
            // lblUrlCap
            //
            this.lblUrlCap.AutoSize = true;
            this.lblUrlCap.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblUrlCap.Location = new System.Drawing.Point(22, 192);
            this.lblUrlCap.Name = "lblUrlCap";
            this.lblUrlCap.Size = new System.Drawing.Size(42, 15);
            this.lblUrlCap.TabIndex = 11;
            this.lblUrlCap.Text = "RTMP:";
            //
            // lblUrl
            //
            this.lblUrl.AutoSize = true;
            this.lblUrl.ForeColor = System.Drawing.Color.FromArgb(91, 140, 255);
            this.lblUrl.Location = new System.Drawing.Point(70, 192);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(10, 15);
            this.lblUrl.TabIndex = 12;
            this.lblUrl.Text = "—";
            //
            // btnToggle
            //
            this.btnToggle.BackColor = System.Drawing.Color.FromArgb(91, 140, 255);
            this.btnToggle.FlatAppearance.BorderSize = 0;
            this.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnToggle.ForeColor = System.Drawing.Color.White;
            this.btnToggle.Location = new System.Drawing.Point(24, 224);
            this.btnToggle.Name = "btnToggle";
            this.btnToggle.Size = new System.Drawing.Size(240, 44);
            this.btnToggle.TabIndex = 13;
            this.btnToggle.Text = "Начать трансляцию";
            this.btnToggle.UseVisualStyleBackColor = false;
            this.btnToggle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click);
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblStatus.Location = new System.Drawing.Point(280, 236);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(80, 15);
            this.lblStatus.TabIndex = 14;
            this.lblStatus.Text = "● остановлено";
            //
            // lblHlsCap
            //
            this.lblHlsCap.AutoSize = true;
            this.lblHlsCap.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.lblHlsCap.Location = new System.Drawing.Point(22, 288);
            this.lblHlsCap.Name = "lblHlsCap";
            this.lblHlsCap.Size = new System.Drawing.Size(104, 15);
            this.lblHlsCap.TabIndex = 15;
            this.lblHlsCap.Text = "Смотреть (HLS):";
            //
            // txtHls
            //
            this.txtHls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHls.BackColor = System.Drawing.Color.FromArgb(30, 36, 48);
            this.txtHls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHls.ForeColor = System.Drawing.Color.FromArgb(232, 236, 243);
            this.txtHls.Location = new System.Drawing.Point(24, 308);
            this.txtHls.Name = "txtHls";
            this.txtHls.ReadOnly = true;
            this.txtHls.Size = new System.Drawing.Size(690, 23);
            this.txtHls.TabIndex = 16;
            //
            // txtLog
            //
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(16, 19, 26);
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.txtLog.ForeColor = System.Drawing.Color.FromArgb(138, 147, 166);
            this.txtLog.Location = new System.Drawing.Point(24, 348);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(712, 170);
            this.txtLog.TabIndex = 17;
            //
            // StreamPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(13, 15, 20);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.txtHls);
            this.Controls.Add(this.lblHlsCap);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnToggle);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.lblUrlCap);
            this.Controls.Add(this.txtFfmpeg);
            this.Controls.Add(this.lblFf);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.numBitrate);
            this.Controls.Add(this.lblBr);
            this.Controls.Add(this.numFps);
            this.Controls.Add(this.lblFps);
            this.Controls.Add(this.cmbMonitor);
            this.Controls.Add(this.lblMon);
            this.Controls.Add(this.lblTitle);
            this.Name = "StreamPanel";
            this.Size = new System.Drawing.Size(760, 540);
            ((System.ComponentModel.ISupportInitialize)(this.numFps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBitrate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMon;
        private System.Windows.Forms.ComboBox cmbMonitor;
        private System.Windows.Forms.Label lblFps;
        private System.Windows.Forms.NumericUpDown numFps;
        private System.Windows.Forms.Label lblBr;
        private System.Windows.Forms.NumericUpDown numBitrate;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label lblFf;
        private System.Windows.Forms.TextBox txtFfmpeg;
        private System.Windows.Forms.Label lblUrlCap;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Button btnToggle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblHlsCap;
        private System.Windows.Forms.TextBox txtHls;
        private System.Windows.Forms.TextBox txtLog;
    }
}
