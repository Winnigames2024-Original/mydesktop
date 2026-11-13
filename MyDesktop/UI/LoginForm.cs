using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyDesktop.Core;

namespace MyDesktop.UI
{
    public partial class LoginForm : Form
    {
        private readonly AppConfig _cfg;
        private readonly ApiClient _api;

        // Конструктор без параметров — для конструктора форм Visual Studio.
        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(AppConfig cfg) : this()
        {
            _cfg = cfg;
            _api = new ApiClient(cfg);
            txtLogin.Text = _cfg.LastLogin ?? "";
            this.Shown += async (s, e) => await CheckFirstRun();
        }

        private async Task CheckFirstRun()
        {
            if (_api == null) return;
            int count = await _api.CountAsync();
            if (count == 0)
            {
                SetMsg("Аккаунтов пока нет — создадим первый.", false);
                OpenWizard(true);
            }
            else if (count < 0)
            {
                SetMsg("Сервер недоступен. Проверьте подключение.", true);
            }
        }

        private void lnkCreate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenWizard(false);
        }

        private void OpenWizard(bool first)
        {
            using (var w = new AccountWizard(_cfg, first))
            {
                if (w.ShowDialog(this) == DialogResult.OK)
                {
                    txtLogin.Text = w.CreatedLogin;
                    SetMsg("Аккаунт создан — войдите.", false);
                    txtPassword.Focus();
                }
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            await DoLogin();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; _ = DoLogin(); }
        }

        private async Task DoLogin()
        {
            if (_api == null) return;
            var login = txtLogin.Text.Trim();
            var pass = txtPassword.Text;
            if (login.Length == 0 || pass.Length == 0) { SetMsg("Введите логин и пароль.", true); return; }

            btnLogin.Enabled = false; btnLogin.Text = "Вход…";
            var r = await _api.LoginAsync(login, pass);
            btnLogin.Enabled = true; btnLogin.Text = "Войти";

            if (r.Ok)
            {
                bool admin = false;
                if (r.Data != null && r.Data.TryGetValue("account", out var accObj)
                    && accObj is System.Collections.Generic.Dictionary<string, object> acc
                    && acc.TryGetValue("admin", out var av) && av is bool bv)
                    admin = bv;
                Session.Set(login, admin);
                _cfg.LastLogin = login; _cfg.Save();
                DialogResult = DialogResult.OK;
                Close();
            }
            else SetMsg(r.Message ?? "Ошибка входа.", true);
        }

        private void SetMsg(string text, bool error)
        {
            lblMsg.ForeColor = error
                ? System.Drawing.Color.FromArgb(255, 91, 110)
                : System.Drawing.Color.FromArgb(61, 220, 132);
            lblMsg.Text = text;
        }
    }
}
