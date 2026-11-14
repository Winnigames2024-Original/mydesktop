using System;
using System.Drawing;
using System.Windows.Forms;
using MyDesktop.Core;

namespace MyDesktop.UI.Panels
{
    public partial class RemotePanel : UserControl
    {
        private readonly AppConfig _cfg;
        private readonly StreamEngine _engine = new StreamEngine();
        private IndicatorForm _indicator;

        // Для конструктора форм Visual Studio.
        public RemotePanel()
        {
            InitializeComponent();
        }

        public RemotePanel(AppConfig cfg) : this()
        {
            _cfg = cfg;
            txtShareKey.Text = (string.IsNullOrEmpty(_cfg.LastLogin) ? "screen" : _cfg.LastLogin) + "-scr";
            string keyShort = _cfg.RustdeskKey.Length > 12 ? _cfg.RustdeskKey.Substring(0, 12) + "…" : _cfg.RustdeskKey;
            lblRelay.Text = "Релей: " + _cfg.RustdeskIdServer + ":" + _cfg.RustdeskIdPort +
                            "   релей-порт :21117\nКлюч сервера: " + keyShort;
            _engine.Log += OnLog;
            _engine.StateChanged += OnState;
            UpdateWatchUrl();
            this.Disposed += (s, e) => { try { _engine.Stop(); } catch { } CloseIndicator(); };
        }

        private string ShareKey => txtShareKey.Text.Trim().Length == 0 ? "screen" : txtShareKey.Text.Trim();

        private void txtShareKey_TextChanged(object sender, EventArgs e) => UpdateWatchUrl();

        private void UpdateWatchUrl()
        {
            if (_cfg == null) return;
            txtWatchUrl.Text = _cfg.HlsBase.TrimEnd('/') + "/" + ShareKey + "/index.m3u8";
        }

        // ── показ своего экрана потоком (как OBS) ──
        private void btnShare_Click(object sender, EventArgs e)
        {
            if (_engine.Running) { _engine.Stop(); return; }
            _cfg.StreamKey = ShareKey; _cfg.Save();
            var opts = new StreamOptions
            {
                FfmpegPath = string.IsNullOrEmpty(_cfg.FfmpegPath) ? "ffmpeg" : _cfg.FfmpegPath,
                Fps = 20,
                BitrateKbps = 2500,
                ScreenBounds = Screen.PrimaryScreen.Bounds,
                RtmpUrl = _cfg.RtmpUrl.TrimEnd('/') + "/" + ShareKey,
                CaptureAudio = false
            };
            _engine.Start(opts);
        }

        private void OnState(bool running)
        {
            if (IsDisposed) return;
            if (InvokeRequired) { try { BeginInvoke((Action)(() => OnState(running))); } catch { } return; }
            btnShare.Text = running ? "Остановить показ" : "Показать экран оператору";
            btnShare.BackColor = running ? Color.FromArgb(255, 91, 110) : Color.FromArgb(91, 140, 255);
            lblShareStatus.ForeColor = running ? Color.FromArgb(61, 220, 132) : Color.FromArgb(138, 147, 166);
            lblShareStatus.Text = running ? "● идёт показ экрана" : "● выключено";
            txtShareKey.Enabled = !running;
            if (running) ShowIndicator(); else CloseIndicator();
        }

        private void OnLog(string line)
        {
            // для удалёнки подробный лог не выводим в UI — статус показывает lblShareStatus
        }

        private void ShowIndicator()
        {
            if (_indicator != null && !_indicator.IsDisposed) return;
            _indicator = new IndicatorForm(() => _engine.Stop());
            _indicator.Show();
        }

        private void CloseIndicator()
        {
            try { if (_indicator != null && !_indicator.IsDisposed) _indicator.Close(); } catch { }
            _indicator = null;
        }

        // ── RustDesk: управление этим ПК ──
        private void btnOpenRustdesk_Click(object sender, EventArgs e)
        {
            string err;
            if (RustDeskManager.LaunchConfigured(_cfg, out err))
                SetRd("RustDesk настроен на ваш релей и запущен. ID и пароль — в окне RustDesk.", false);
            else
                SetRd(err, true);
        }

        private void btnDownload_Click(object sender, EventArgs e) => RustDeskManager.OpenDownloadPage();

        // ── оператор ──
        private void btnWatch_Click(object sender, EventArgs e)
        {
            var key = txtPeerKey.Text.Trim();
            if (key.Length == 0) { SetRd("Укажите ключ сессии для просмотра.", true); return; }
            RustDeskManager.OpenUrl(_cfg.HlsBase.TrimEnd('/') + "/" + key + "/index.m3u8");
        }

        private void btnControl_Click(object sender, EventArgs e)
        {
            var id = txtPeerId.Text.Trim();
            string err;
            if (RustDeskManager.LaunchConfigured(_cfg, out err, string.IsNullOrEmpty(id) ? null : id))
                SetRd("RustDesk запущен" + (id.Length > 0 ? " для подключения к " + id : "") + ".", false);
            else
                SetRd(err, true);
        }

        private void SetRd(string text, bool error)
        {
            lblRdStatus.ForeColor = error ? Color.FromArgb(255, 91, 110) : Color.FromArgb(61, 220, 132);
            lblRdStatus.Text = text;
        }
    }
}
