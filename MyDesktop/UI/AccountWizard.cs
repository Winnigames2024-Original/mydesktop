using System;
using System.Drawing;
using System.Windows.Forms;
using MyDesktop.Core;
using MyDesktop.UI.Controls;

namespace MyDesktop.UI
{
    /// <summary>Мастер создания аккаунта (при первом старте, если аккаунтов нет).</summary>
    public class AccountWizard : FormBase
    {
        private readonly AppConfig _cfg;
        private readonly ApiClient _api;
        private readonly bool _first;
        private RoundedTextBox _login, _pass, _pass2;
        private RoundedButton _create;
        private Label _msg;

        public string CreatedLogin { get; private set; }

        public AccountWizard(AppConfig cfg, bool first)
        {
            _cfg = cfg; _api = new ApiClient(cfg); _first = first;
            TitleText = first ? "MyDesktop — первый аккаунт" : "MyDesktop — новый аккаунт";
            ShowMinimize = false;
            Size = new Size(420, 560);
            BuildUi();
        }

        private void BuildUi()
        {
            var card = new CardPanel { Fill = Theme.Surface, Location = new Point(30, 70), Size = new Size(360, 460) };
            Controls.Add(card);

            card.Controls.Add(new Label
            {
                Text = _first ? "Создание аккаунта" : "Новый аккаунт",
                Font = Theme.FontH1, ForeColor = Theme.Text, AutoSize = false,
                Location = new Point(28, 26), Size = new Size(304, 34), BackColor = Color.Transparent
            });
            card.Controls.Add(new Label
            {
                Text = _first ? "Это будет администратор." : "Данные сохранятся на сервере (scrypt).",
                Font = Theme.FontSmall, ForeColor = Theme.Muted, AutoSize = false,
                Location = new Point(28, 60), Size = new Size(304, 18), BackColor = Color.Transparent
            });

            card.Controls.Add(Lbl("Логин", 94));
            _login = new RoundedTextBox { Location = new Point(28, 116), Size = new Size(304, 44) };
            card.Controls.Add(_login);

            card.Controls.Add(Lbl("Пароль", 168));
            _pass = new RoundedTextBox { Location = new Point(28, 190), Size = new Size(304, 44), UseSystemPasswordChar = true };
            card.Controls.Add(_pass);

            card.Controls.Add(Lbl("Повторите пароль", 242));
            _pass2 = new RoundedTextBox { Location = new Point(28, 264), Size = new Size(304, 44), UseSystemPasswordChar = true };
            card.Controls.Add(_pass2);

            _create = new RoundedButton { Text = "Создать", Location = new Point(28, 324), Size = new Size(304, 46), Radius = 13 };
            _create.Click += (s, e) => DoCreate();
            card.Controls.Add(_create);

            _msg = new Label
            {
                Text = "", AutoSize = false, Location = new Point(28, 378), Size = new Size(304, 60),
                Font = Theme.FontSmall, ForeColor = Theme.Danger, BackColor = Color.Transparent
            };
            card.Controls.Add(_msg);
        }

        private Label Lbl(string t, int y) => new Label
        {
            Text = t, Font = Theme.FontSmall, ForeColor = Theme.Muted, AutoSize = false,
            Location = new Point(28, y), Size = new Size(240, 18), BackColor = Color.Transparent
        };

        private async void DoCreate()
        {
            var login = _login.Inner.Text.Trim();
            var p1 = _pass.Inner.Text; var p2 = _pass2.Inner.Text;
            if (login.Length < 3) { Err("Логин минимум 3 символа."); return; }
            if (p1.Length < 6) { Err("Пароль минимум 6 символов."); return; }
            if (p1 != p2) { Err("Пароли не совпадают."); return; }

            _create.Enabled = false; _create.Text = "Создаю…";
            var r = await _api.RegisterAsync(login, p1);
            _create.Enabled = true; _create.Text = "Создать";

            if (r.Ok)
            {
                CreatedLogin = login;
                DialogResult = DialogResult.OK;
                Close();
            }
            else Err(r.Message ?? "Не удалось создать аккаунт.");
        }

        private void Err(string t) { _msg.ForeColor = Theme.Danger; _msg.Text = t; }
    }
}
