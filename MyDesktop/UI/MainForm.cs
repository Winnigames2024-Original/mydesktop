using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyDesktop.Core;
using MyDesktop.UI.Panels;

namespace MyDesktop.UI
{
    public partial class MainForm : Form
    {
        private readonly AppConfig _cfg;
        private StreamPanel _stream;
        private RemotePanel _remote;
        private EffectsPanel _effects;

        // Для конструктора форм Visual Studio.
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(AppConfig cfg) : this()
        {
            _cfg = cfg;
            lblUser.Text = "● " + (Session.Login ?? "") + (Session.IsAdmin ? "  (админ)" : "");

            // панель трансляции встраиваем в область контента (поверх текстовых заглушек)
            _stream = new StreamPanel(_cfg) { Dock = DockStyle.Fill, Visible = false };
            pnlContent.Controls.Add(_stream);
            _remote = new RemotePanel(_cfg) { Dock = DockStyle.Fill, Visible = false };
            pnlContent.Controls.Add(_remote);
            _effects = new EffectsPanel { Dock = DockStyle.Fill, Visible = false };
            pnlContent.Controls.Add(_effects);

            this.Shown += async (s, e) => await ShowSection("Главная");
        }

        private async void btnNav_Click(object sender, EventArgs e)
        {
            var tag = (sender as Control)?.Tag as string ?? "Главная";
            await ShowSection(tag);
        }

        private async Task ShowSection(string name)
        {
            bool stream = (name == "Трансляция");
            bool remote = (name == "Удалённый доступ");
            bool effects = (name == "Эффекты");
            if (_stream != null) { _stream.Visible = stream; if (stream) _stream.BringToFront(); }
            if (_remote != null) { _remote.Visible = remote; if (remote) _remote.BringToFront(); }
            if (_effects != null) { _effects.Visible = effects; if (effects) _effects.BringToFront(); }
            bool panel = stream || remote || effects;
            lblContentTitle.Visible = lblContentText.Visible = !panel;
            lblStatus.Visible = (name == "Главная");
            switch (name)
            {
                case "Главная":
                    lblContentTitle.Text = "Главная";
                    lblContentText.Text = "MyDesktop — трансляция, локальные эффекты и прозрачный удалённый доступ.";
                    await RefreshStatus();
                    break;
                case "Трансляция":
                    lblContentTitle.Text = "Трансляция «как OBS»";
                    lblContentText.Text = "RTMP-пуш экрана на rtmp://" + AppConfig.VpsIp + ":1936/live. Модуль собираем в следующем этапе.";
                    break;
                case "Эффекты":
                    lblContentTitle.Text = "Локальные эффекты";
                    lblContentText.Text = "Эффекты на своём ПК (вкл/выкл, можно несколько сразу). Модуль собираем в следующем этапе.";
                    break;
                case "Удалённый доступ":
                    lblContentTitle.Text = "Удалённый доступ (RustDesk)";
                    lblContentText.Text = "Агент RustDesk к своему релею " + AppConfig.VpsIp + ": ID и пароль. Модуль собираем в следующем этапе.";
                    break;
                case "Аккаунт":
                    lblContentTitle.Text = "Аккаунт";
                    lblContentText.Text = "Вы вошли как: " + Session.Login + (Session.IsAdmin ? " (администратор)" : "");
                    break;
            }
        }

        private async Task RefreshStatus()
        {
            if (_cfg == null) return;
            var api = new ApiClient(_cfg);
            int c = await api.CountAsync();
            if (c < 0)
            {
                lblStatus.ForeColor = System.Drawing.Color.FromArgb(255, 91, 110);
                lblStatus.Text = "● Сервер недоступен (" + _cfg.BackendBase + ")";
            }
            else
            {
                lblStatus.ForeColor = System.Drawing.Color.FromArgb(61, 220, 132);
                lblStatus.Text = "● Сервер на связи · аккаунтов: " + c;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Application.Restart();
        }
    }
}
