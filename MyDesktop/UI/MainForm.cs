using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyDesktop.Core;

namespace MyDesktop.UI
{
    public partial class MainForm : Form
    {
        private readonly AppConfig _cfg;

        // Для конструктора форм Visual Studio.
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(AppConfig cfg) : this()
        {
            _cfg = cfg;
            lblUser.Text = "● " + (Session.Login ?? "") + (Session.IsAdmin ? "  (админ)" : "");
            this.Shown += async (s, e) => await ShowSection("Главная");
        }

        private async void btnNav_Click(object sender, EventArgs e)
        {
            var tag = (sender as Control)?.Tag as string ?? "Главная";
            await ShowSection(tag);
        }

        private async Task ShowSection(string name)
        {
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
