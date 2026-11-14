using System;
using System.Drawing;
using System.Windows.Forms;
using MyDesktop.Core;

namespace MyDesktop.UI.Panels
{
    public partial class StreamPanel : UserControl
    {
        private readonly AppConfig _cfg;
        private readonly StreamEngine _engine = new StreamEngine();

        // Пункт списка мониторов (хранит границы захвата).
        private class MonitorItem
        {
            public string Text;
            public Rectangle Bounds;
            public override string ToString() => Text;
        }

        // Для конструктора форм Visual Studio.
        public StreamPanel()
        {
            InitializeComponent();
        }

        public StreamPanel(AppConfig cfg) : this()
        {
            _cfg = cfg;

            // мониторы
            int i = 1;
            foreach (var sc in Screen.AllScreens)
            {
                var b = sc.Bounds;
                cmbMonitor.Items.Add(new MonitorItem
                {
                    Text = $"Монитор {i}{(sc.Primary ? " (осн.)" : "")} — {b.Width}x{b.Height}",
                    Bounds = b
                });
                i++;
            }
            if (cmbMonitor.Items.Count > 0) cmbMonitor.SelectedIndex = 0;

            txtFfmpeg.Text = string.IsNullOrEmpty(_cfg.FfmpegPath) ? "ffmpeg" : _cfg.FfmpegPath;
            txtKey.Text = string.IsNullOrEmpty(_cfg.StreamKey) ? _cfg.LastLogin : _cfg.StreamKey;

            _engine.Log += OnLog;
            _engine.StateChanged += OnState;

            UpdateUrls();
            this.Disposed += (s, e) => { try { _engine.Stop(); } catch { } };
        }

        private void txtKey_TextChanged(object sender, EventArgs e) => UpdateUrls();

        private string Key => txtKey.Text.Trim().Length == 0 ? "stream" : txtKey.Text.Trim();

        private void UpdateUrls()
        {
            if (_cfg == null) return;
            lblUrl.Text = _cfg.RtmpUrl.TrimEnd('/') + "/" + Key;
            txtHls.Text = _cfg.HlsBase.TrimEnd('/') + "/" + Key + "/index.m3u8";
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            if (_engine.Running) { _engine.Stop(); return; }

            var mon = cmbMonitor.SelectedItem as MonitorItem;
            if (mon == null) { OnLog("Нет доступного монитора."); return; }

            // сохраняем выбор
            _cfg.StreamKey = Key;
            _cfg.FfmpegPath = txtFfmpeg.Text.Trim();
            _cfg.Save();

            var opts = new StreamOptions
            {
                FfmpegPath = _cfg.FfmpegPath,
                Fps = (int)numFps.Value,
                BitrateKbps = (int)numBitrate.Value,
                ScreenBounds = mon.Bounds,
                RtmpUrl = lblUrl.Text,
                CaptureAudio = false
            };
            txtLog.Clear();
            _engine.Start(opts);
        }

        // события движка приходят из другого потока — маршалим в UI
        private void OnLog(string line)
        {
            if (IsDisposed) return;
            if (InvokeRequired) { try { BeginInvoke((Action)(() => OnLog(line))); } catch { } return; }
            if (txtLog.Lines.Length > 400)
                txtLog.Text = string.Join(Environment.NewLine, txtLog.Lines, 200, txtLog.Lines.Length - 200);
            txtLog.AppendText(line + Environment.NewLine);
        }

        private void OnState(bool running)
        {
            if (IsDisposed) return;
            if (InvokeRequired) { try { BeginInvoke((Action)(() => OnState(running))); } catch { } return; }
            btnToggle.Text = running ? "Остановить" : "Начать трансляцию";
            btnToggle.BackColor = running ? Color.FromArgb(255, 91, 110) : Color.FromArgb(91, 140, 255);
            lblStatus.ForeColor = running ? Color.FromArgb(61, 220, 132) : Color.FromArgb(138, 147, 166);
            lblStatus.Text = running ? "● в эфире" : "● остановлено";
            cmbMonitor.Enabled = numFps.Enabled = numBitrate.Enabled = txtKey.Enabled = txtFfmpeg.Enabled = !running;
        }
    }
}
