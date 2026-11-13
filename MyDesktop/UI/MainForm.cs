using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MyDesktop.Core;
using MyDesktop.UI.Controls;
using MyDesktop.UI.Panels;

namespace MyDesktop.UI
{
    /// <summary>Главное окно: левая навигация + область контента.</summary>
    public class MainForm : FormBase
    {
        private readonly AppConfig _cfg;
        private Panel _content;
        private readonly List<RoundedButton> _nav = new List<RoundedButton>();
        private readonly Dictionary<string, Func<Control>> _factories = new Dictionary<string, Func<Control>>();
        private readonly Dictionary<string, Control> _cache = new Dictionary<string, Control>();

        public MainForm(AppConfig cfg)
        {
            _cfg = cfg;
            TitleText = "MyDesktop";
            Size = new Size(1000, 640);
            MinimumSize = new Size(900, 560);
            BuildUi();
        }

        private void BuildUi()
        {
            // левая колонка-навигация
            var side = new CardPanel
            {
                Fill = Theme.Surface, Border = false,
                Location = new Point(14, TitleBarHeight + 12),
                Size = new Size(210, Height - TitleBarHeight - 26)
            };
            side.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            Controls.Add(side);

            _content = new Panel
            {
                BackColor = Theme.Bg,
                Location = new Point(236, TitleBarHeight + 12),
                Size = new Size(Width - 250, Height - TitleBarHeight - 26),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            Controls.Add(_content);

            // фабрики панелей (создаются лениво)
            _factories["Главная"]          = () => new HomePanel(_cfg);
            _factories["Трансляция"]       = () => Placeholder("Трансляция «как OBS»", "RTMP-пуш на rtmp://" + AppConfig.VpsIp + ":1936/live — в следующем этапе сборки.");
            _factories["Эффекты"]          = () => Placeholder("Локальные эффекты", "Эффекты на своём ПК (вкл/выкл) — в следующем этапе сборки.");
            _factories["Удалённый доступ"] = () => Placeholder("Удалённый доступ (RustDesk)", "Интеграция агента RustDesk к своему релею — в следующем этапе сборки.");
            _factories["Аккаунт"]          = () => AccountPanel();

            string[] items = { "Главная", "Трансляция", "Эффекты", "Удалённый доступ", "Аккаунт" };
            int y = 18;
            foreach (var name in items)
            {
                var b = new RoundedButton
                {
                    Text = "   " + name, Ghost = true, Radius = 11,
                    Location = new Point(14, y), Size = new Size(182, 44),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                b.Click += (s, e) => Navigate(name);
                side.Controls.Add(b);
                _nav.Add(b);
                y += 50;
            }

            // низ навигации — пользователь
            var who = new Label
            {
                Text = "● " + (Session.Login ?? "") + (Session.IsAdmin ? "  (админ)" : ""),
                Font = Theme.FontSmall, ForeColor = Theme.Ok, AutoSize = false,
                Location = new Point(16, side.Height - 34), Size = new Size(180, 20),
                BackColor = Color.Transparent, Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            side.Controls.Add(who);

            Navigate("Главная");
        }

        private void Navigate(string name)
        {
            foreach (var b in _nav) b.Active = ("   " + name) == b.Text;
            foreach (var b in _nav) b.Invalidate();

            if (!_cache.TryGetValue(name, out var ctrl))
            {
                ctrl = _factories[name]();
                ctrl.Dock = DockStyle.Fill;
                _cache[name] = ctrl;
            }
            _content.Controls.Clear();
            _content.Controls.Add(ctrl);
        }

        private Control Placeholder(string title, string text)
        {
            var p = new CardPanel { Fill = Theme.Surface };
            p.Controls.Add(new Label
            {
                Text = title, Font = Theme.FontH1, ForeColor = Theme.Text, AutoSize = false,
                Location = new Point(28, 28), Size = new Size(600, 36), BackColor = Color.Transparent
            });
            p.Controls.Add(new Label
            {
                Text = text, Font = Theme.FontBody, ForeColor = Theme.Muted, AutoSize = false,
                Location = new Point(28, 72), Size = new Size(600, 60), BackColor = Color.Transparent
            });
            return p;
        }

        private Control AccountPanel()
        {
            var p = new CardPanel { Fill = Theme.Surface };
            p.Controls.Add(new Label
            {
                Text = "Аккаунт", Font = Theme.FontH1, ForeColor = Theme.Text, AutoSize = false,
                Location = new Point(28, 28), Size = new Size(400, 36), BackColor = Color.Transparent
            });
            p.Controls.Add(new Label
            {
                Text = "Вы вошли как: " + Session.Login, Font = Theme.FontBody, ForeColor = Theme.Muted,
                AutoSize = false, Location = new Point(28, 76), Size = new Size(400, 24), BackColor = Color.Transparent
            });
            var logout = new RoundedButton { Text = "Выйти", Location = new Point(28, 120), Size = new Size(200, 44), Radius = 12 };
            logout.Click += (s, e) => { Session.Clear(); Application.Restart(); };
            p.Controls.Add(logout);
            return p;
        }
    }
}
