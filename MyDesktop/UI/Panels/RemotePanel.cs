using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyDesktop.Core;

namespace MyDesktop.UI.Panels
{
    public partial class RemotePanel : UserControl
    {
        private readonly AppConfig _cfg;
        private readonly ApiClient _api;
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
            _api = new ApiClient(cfg);
            lblMyLogin.Text = string.IsNullOrEmpty(Session.Login) ? (_cfg.LastLogin ?? "—") : Session.Login;
            _engine.StateChanged += OnState;
            this.Disposed += (s, e) => { try { _engine.Stop(); } catch { } CloseIndicator(); };
            RefreshPeers();
        }

        private void RefreshPeers()
        {
            lstPeers.Items.Clear();
            foreach (var p in _cfg.Peers) lstPeers.Items.Add(p);
        }

        private void SavePeer(string login, string pass)
        {
            var key = login.ToLowerInvariant();
            RemotePeer found = null;
            foreach (var p in _cfg.Peers) if (p.Login.ToLowerInvariant() == key) { found = p; break; }
            if (found == null) { found = new RemotePeer { Login = login }; _cfg.Peers.Add(found); }
            found.Pw = Secret.Protect(pass);
            while (_cfg.Peers.Count > 30) _cfg.Peers.RemoveAt(0);
            _cfg.Save();
            RefreshPeers();
        }

        private string MyKey => (string.IsNullOrEmpty(Session.Login) ? _cfg.LastLogin : Session.Login) ?? "screen";

        // ── разрешить просмотр своего экрана (поток под своим логином) ──
        private void btnAllow_Click(object sender, EventArgs e)
        {
            if (_engine.Running) { _engine.Stop(); return; }
            var opts = new StreamOptions
            {
                FfmpegPath = string.IsNullOrEmpty(_cfg.FfmpegPath) ? "ffmpeg" : _cfg.FfmpegPath,
                Fps = 20,
                BitrateKbps = 2500,
                ScreenBounds = Screen.PrimaryScreen.Bounds,
                RtmpUrl = _cfg.RtmpUrl.TrimEnd('/') + "/" + MyKey,
                CaptureAudio = false
            };
            _engine.Start(opts);
        }

        private void OnState(bool running)
        {
            if (IsDisposed) return;
            if (InvokeRequired) { try { BeginInvoke((Action)(() => OnState(running))); } catch { } return; }
            btnAllow.Text = running ? "Остановить показ" : "Разрешить просмотр моего экрана";
            btnAllow.BackColor = running ? Color.FromArgb(255, 91, 110) : Color.FromArgb(91, 140, 255);
            lblAllowStatus.ForeColor = running ? Color.FromArgb(61, 220, 132) : Color.FromArgb(138, 147, 166);
            lblAllowStatus.Text = running ? "● идёт показ экрана (по вашему логину)" : "● выключено";
            if (running) ShowIndicator(); else CloseIndicator();
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

        // ── подключиться к другому ПК по логину и паролю ──
        private async void btnConnect_Click(object sender, EventArgs e)
            => await DoConnect(txtLogin.Text.Trim(), txtPass.Text);

        private async void btnQuick_Click(object sender, EventArgs e)
        {
            var peer = lstPeers.SelectedItem as RemotePeer;
            if (peer == null) { SetStatus("Выберите подключение из списка.", true); return; }
            var pw = Secret.Unprotect(peer.Pw);
            txtLogin.Text = peer.Login;
            txtPass.Text = pw;
            await DoConnect(peer.Login, pw);
        }

        private void btnForget_Click(object sender, EventArgs e)
        {
            var peer = lstPeers.SelectedItem as RemotePeer;
            if (peer == null) return;
            _cfg.Peers.Remove(peer);
            _cfg.Save();
            RefreshPeers();
        }

        private async Task DoConnect(string login, string pass)
        {
            if (login.Length == 0 || pass.Length == 0) { SetStatus("Введите логин и пароль.", true); return; }

            btnConnect.Enabled = false; btnQuick.Enabled = false; btnConnect.Text = "Подключение…";
            var r = await _api.LoginAsync(login, pass);   // проверка логина+пароля на бэкенде (scrypt)
            btnConnect.Enabled = true; btnQuick.Enabled = true; btnConnect.Text = "Подключиться";

            if (!r.Ok) { SetStatus(r.Message ?? "Неверный логин или пароль.", true); return; }

            SavePeer(login, pass);   // запоминаем для быстрого подключения

            // смотрим экран этого логина плеером ffplay по RTMP напрямую (без браузера, без задержки)
            var rtmp = _cfg.RtmpUrl.TrimEnd('/') + "/" + login;
            var ffplay = FfplayPath();
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = ffplay,
                    Arguments = "-window_title \"MyDesktop — " + login + "\" -autoexit -fflags nobuffer -flags low_delay \"" + rtmp + "\"",
                    UseShellExecute = false,
                    CreateNoWindow = false
                });
                SetStatus("Подключено. Открываю экран (" + login + ") в плеере…", false);
            }
            catch (Exception ex)
            {
                SetStatus("Не удалось открыть плеер ffplay (" + ex.Message + "). Проверьте, что в ffmpeg есть ffplay.exe. Поток: " + rtmp, true);
            }
        }

        // ffplay рядом с ffmpeg, либо из PATH
        private string FfplayPath()
        {
            var f = _cfg.FfmpegPath;
            if (!string.IsNullOrEmpty(f) && f.IndexOf("ffmpeg", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                try
                {
                    var dir = Path.GetDirectoryName(f);
                    if (!string.IsNullOrEmpty(dir))
                    {
                        var cand = Path.Combine(dir, "ffplay.exe");
                        if (File.Exists(cand)) return cand;
                    }
                }
                catch { }
            }
            return "ffplay";
        }

        private void SetStatus(string text, bool error)
        {
            lblConnStatus.ForeColor = error ? Color.FromArgb(255, 91, 110) : Color.FromArgb(61, 220, 132);
            lblConnStatus.Text = text;
        }
    }
}
