using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyDesktop.Core;
using MyDesktop.UI.Controls;
using Newtonsoft.Json.Linq;

namespace MyDesktop.UI
{
    public class LoginForm : FormBase
    {
        private readonly AppConfig _cfg;
        private readonly ApiClient _api;
        private RoundedTextBox _login, _pass;
        private RoundedButton _submit;
        private Label _msg;
        private LinkLabel _createLink;

        public LoginForm(AppConfig cfg)
        {
            _cfg = cfg;
            _api = new ApiClient(cfg);
            TitleText = "MyDesktop — вход";
            ShowMinimize = true;
            Size = new Size(420, 520);
            BuildUi();
            Shown += async (s, e) => await CheckFirstRun();
        }

        private void BuildUi()
        {
            var card = new CardPanel { Fill = Theme.Surface, Location = new Point(30, 70), Size = new Size(360, 420) };
            Controls.Add(card);

            var h = new Label
            {
                Text = "С возвращением",
                Font = Theme.FontH1, ForeColor = Theme.Text, AutoSize = false,
                Location = new Point(28, 28), Size = new Size(300, 36), BackColor = Color.Transparent
            };
            var sub = new Label
            {
                Text = "Войдите в свой аккаунт MyDesktop",
                Font = Theme.FontSmall, ForeColor = Theme.Muted, AutoSize = false,
                Location = new Point(28, 64), Size = new Size(300, 20), BackColor = Color.Transparent
            };
            card.Controls.Add(h); card.Controls.Add(sub);

            card.Controls.Add(MakeLabel("Логин", 100));
            _login = new RoundedTextBox { Location = new Point(28, 122), Size = new Size(304, 44) };
            _login.Inner.Text = _cfg.LastLogin ?? "";
            card.Controls.Add(_login);

            card.Controls.Add(MakeLabel("Пароль", 176));
            _pass = new RoundedTextBox { Location = new Point(28, 198), Size = new Size(304, 44), UseSystemPasswordChar = true };
            _pass.Inner.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) DoLogin(); };
            card.Controls.Add(_pass);

            _submit = new RoundedButton { Text = "Войти", Location = new Point(28, 262), Size = new Size(304, 46), Radius = 13 };
            _submit.Click += (s, e) => DoLogin();
            card.Controls.Add(_submit);

            _msg = new Label
            {
                Text = "", AutoSize = false, Location = new Point(28, 318), Size = new Size(304, 40),
                Font = Theme.FontSmall, ForeColor = Theme.Danger, BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft
            };
            card.Controls.Add(_msg);

            _createLink = new LinkLabel
            {
                Text = "Создать новый аккаунт",
                LinkColor = Theme.Accent, ActiveLinkColor = Theme.Accent2,
                Font = Theme.FontSmall, AutoSize = true, BackColor = Color.Transparent,
                Location = new Point(28, 372)
            };
            _createLink.LinkClicked += (s, e) => OpenWizard(false);
            card.Controls.Add(_createLink);
        }

        private Label MakeLabel(string text, int y) => new Label
        {
            Text = text, Font = Theme.FontSmall, ForeColor = Theme.Muted, AutoSize = false,
            Location = new Point(28, y), Size = new Size(200, 18), BackColor = Color.Transparent
        };

        private async Task CheckFirstRun()
        {
            int count = await _api.CountAsync();
            if (count == 0)
            {
                // аккаунтов нет — сразу мастер создания первого
                SetMsg("Аккаунтов пока нет — создадим первый.", false);
                OpenWizard(true);
            }
            else if (count < 0)
            {
                SetMsg("Сервер недоступен. Проверьте подключение.", true);
            }
        }

        private void OpenWizard(bool first)
        {
            using (var w = new AccountWizard(_cfg, first))
            {
                if (w.ShowDialog(this) == DialogResult.OK)
                {
                    _login.Inner.Text = w.CreatedLogin;
                    SetMsg("Аккаунт создан — войдите.", false);
                    _pass.Inner.Focus();
                }
            }
        }

        private async void DoLogin()
        {
            var login = _login.Inner.Text.Trim();
            var pass = _pass.Inner.Text;
            if (login.Length == 0 || pass.Length == 0) { SetMsg("Введите логин и пароль.", true); return; }

            _submit.Enabled = false; _submit.Text = "Вход…";
            var r = await _api.LoginAsync(login, pass);
            _submit.Enabled = true; _submit.Text = "Войти";

            if (r.Ok)
            {
                bool admin = r.Data?["account"]?["admin"]?.Value<bool>() ?? false;
                Session.Set(login, admin);
                _cfg.LastLogin = login; _cfg.Save();
                DialogResult = DialogResult.OK;
                Close();
            }
            else SetMsg(r.Message ?? "Ошибка входа.", true);
        }

        private void SetMsg(string text, bool error)
        {
            _msg.ForeColor = error ? Theme.Danger : Theme.Ok;
            _msg.Text = text;
        }
    }
}
