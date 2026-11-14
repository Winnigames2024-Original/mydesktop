using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MyDesktop.Core.Effects;

namespace MyDesktop.UI.Panels
{
    /// <summary>
    /// Локальные эффекты на СВОём ПК. Сверху — панель кнопок (ToolStrip),
    /// каждая кнопка включает/выключает эффект; можно несколько сразу.
    /// </summary>
    public class EffectsPanel : UserControl
    {
        private readonly List<KeyValuePair<ToolStripButton, IEffect>> _map
            = new List<KeyValuePair<ToolStripButton, IEffect>>();
        private readonly Timer _sync = new Timer { Interval = 500 };
        private bool _building;

        public EffectsPanel()
        {
            BackColor = Color.FromArgb(13, 15, 20);

            // ── верхняя панель-кнопки ──
            var ts = new ToolStrip
            {
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(22, 26, 34),
                ForeColor = Color.FromArgb(232, 236, 243),
                GripStyle = ToolStripGripStyle.Hidden,
                Renderer = new ToolStripProfessionalRenderer(new DarkColors()),
                Padding = new Padding(6, 4, 6, 4),
                ImageScalingSize = new Size(0, 0),
                AutoSize = false,
                Height = 48
            };

            IEffect[] effects =
            {
                new FogEffect(), new TrainEffect(), new FlipScreenEffect(), new LinesEffect(),
                new WindowsJumpEffect(), new CursorShakeEffect(), new CursorJumpEffect(),
                new BsodJokeEffect(), new ShowImageEffect()
            };
            foreach (var ef in effects)
            {
                var b = new ToolStripButton(ef.Name)
                {
                    DisplayStyle = ToolStripItemDisplayStyle.Text,
                    CheckOnClick = false,
                    ForeColor = Color.FromArgb(232, 236, 243),
                    Font = new Font("Segoe UI", 9.5f),
                    Padding = new Padding(6, 2, 6, 2)
                };
                var captured = ef;
                b.Click += (s, e) => Toggle(b, captured);
                ts.Items.Add(b);
                _map.Add(new KeyValuePair<ToolStripButton, IEffect>(b, ef));
            }

            // ── заголовок + подсказка ──
            var title = new Label
            {
                Text = "Локальные эффекты (на вашем ПК)",
                Font = new Font("Segoe UI Semibold", 18f, FontStyle.Bold),
                ForeColor = Color.FromArgb(232, 236, 243),
                AutoSize = true, Location = new Point(20, 64), BackColor = Color.Transparent
            };
            var note = new Label
            {
                Text = "Нажимайте кнопки сверху — эффекты включаются/выключаются, можно несколько сразу.\n" +
                       "«Переворот экрана» сам вернётся через 8 секунд. «Синий экран» и «Картинка» закрываются крестиком.\n" +
                       "Всё работает только на этом компьютере.",
                Font = new Font("Segoe UI", 9.5f), ForeColor = Color.FromArgb(138, 147, 166),
                AutoSize = false, Location = new Point(22, 104), Size = new Size(680, 70), BackColor = Color.Transparent
            };

            Controls.Add(note);
            Controls.Add(title);
            Controls.Add(ts); // добавляем ToolStrip последним, чтобы Dock.Top встал сверху

            _sync.Tick += (s, e) => SyncButtons();
            _sync.Start();

            this.Disposed += (s, e) => { _sync.Stop(); StopAll(); };
        }

        private void Toggle(ToolStripButton b, IEffect ef)
        {
            try
            {
                if (!ef.Active) ef.Start(); else ef.Stop();
            }
            catch { }
            Paint(b, ef.Active);
        }

        private void SyncButtons()
        {
            if (_building) return;
            foreach (var kv in _map)
                Paint(kv.Key, kv.Value.Active);
        }

        private void Paint(ToolStripButton b, bool active)
        {
            b.Checked = active;
            b.ForeColor = active ? Color.White : Color.FromArgb(232, 236, 243);
            b.BackColor = active ? Color.FromArgb(91, 140, 255) : Color.FromArgb(22, 26, 34);
        }

        private void StopAll()
        {
            foreach (var kv in _map) { try { if (kv.Value.Active) kv.Value.Stop(); } catch { } }
        }

        // тёмная окраска ToolStrip
        private class DarkColors : ProfessionalColorTable
        {
            public override Color ToolStripGradientBegin => Color.FromArgb(22, 26, 34);
            public override Color ToolStripGradientMiddle => Color.FromArgb(22, 26, 34);
            public override Color ToolStripGradientEnd => Color.FromArgb(22, 26, 34);
            public override Color ButtonSelectedHighlight => Color.FromArgb(30, 36, 48);
            public override Color ButtonSelectedGradientBegin => Color.FromArgb(30, 36, 48);
            public override Color ButtonSelectedGradientEnd => Color.FromArgb(30, 36, 48);
            public override Color ButtonPressedGradientBegin => Color.FromArgb(91, 140, 255);
            public override Color ButtonPressedGradientEnd => Color.FromArgb(91, 140, 255);
            public override Color ButtonCheckedGradientBegin => Color.FromArgb(91, 140, 255);
            public override Color ButtonCheckedGradientEnd => Color.FromArgb(91, 140, 255);
        }
    }
}
