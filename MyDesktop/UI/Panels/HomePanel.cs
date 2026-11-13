using System;
using System.Drawing;
using System.Windows.Forms;
using MyDesktop.Core;
using MyDesktop.UI.Controls;

namespace MyDesktop.UI.Panels
{
    /// <summary>Домашняя панель: приветствие + плитки-разделы + статус сервера.</summary>
    public class HomePanel : CardPanel
    {
        private readonly AppConfig _cfg;
        private Label _status;

        public HomePanel(AppConfig cfg)
        {
            _cfg = cfg;
            Fill = Theme.Bg; Border = false;
            Build();
            Shown();
        }

        private void Build()
        {
            Controls.Add(new Label
            {
                Text = "Привет, " + (Session.Login ?? "") + " 👋",
                Font = Theme.FontH1, ForeColor = Theme.Text, AutoSize = false,
                Location = new Point(6, 6), Size = new Size(700, 38), BackColor = Color.Transparent
            });
            Controls.Add(new Label
            {
                Text = "MyDesktop — трансляция, локальные эффекты и прозрачный удалённый доступ.",
                Font = Theme.FontBody, ForeColor = Theme.Muted, AutoSize = false,
                Location = new Point(6, 46), Size = new Size(700, 22), BackColor = Color.Transparent
            });

            Tile("Трансляция", "Пуш экрана на свой RTMP-сервер (как OBS).", Theme.Accent, 6, 86);
            Tile("Эффекты", "Весёлые эффекты на своём ПК — вкл/выкл.", Theme.Accent2, 300, 86);
            Tile("Удалённый доступ", "RustDesk к своему релею: ID + пароль.", Theme.Ok, 6, 210);
            Tile("Аккаунт", "Вход, создание и удаление аккаунтов.", Theme.Danger, 300, 210);

            _status = new Label
            {
                Text = "Проверяю сервер…", Font = Theme.FontSmall, ForeColor = Theme.Muted,
                AutoSize = false, Location = new Point(6, 340), Size = new Size(700, 20), BackColor = Color.Transparent
            };
            Controls.Add(_status);
        }

        private void Tile(string title, string desc, Color accent, int x, int y)
        {
            var card = new CardPanel { Fill = Theme.Surface, Location = new Point(x, y), Size = new Size(280, 108) };
            var bar = new Panel { BackColor = accent, Location = new Point(16, 18), Size = new Size(4, 72) };
            card.Controls.Add(bar);
            card.Controls.Add(new Label
            {
                Text = title, Font = Theme.FontH2, ForeColor = Theme.Text, AutoSize = false,
                Location = new Point(32, 18), Size = new Size(230, 24), BackColor = Color.Transparent
            });
            card.Controls.Add(new Label
            {
                Text = desc, Font = Theme.FontSmall, ForeColor = Theme.Muted, AutoSize = false,
                Location = new Point(32, 46), Size = new Size(234, 50), BackColor = Color.Transparent
            });
            Controls.Add(card);
        }

        private async void Shown()
        {
            var api = new ApiClient(_cfg);
            int c = await api.CountAsync();
            if (c < 0) { _status.ForeColor = Theme.Danger; _status.Text = "● Сервер недоступен (" + _cfg.BackendBase + ")"; }
            else { _status.ForeColor = Theme.Ok; _status.Text = "● Сервер на связи · аккаунтов: " + c; }
        }
    }
}
