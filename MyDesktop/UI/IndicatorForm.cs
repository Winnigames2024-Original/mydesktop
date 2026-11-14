using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyDesktop.UI
{
    /// <summary>
    /// Видимый индикатор активной сессии (баннер поверх всех окон).
    /// Показывается, пока экран транслируется оператору — прозрачность удалёнки.
    /// </summary>
    public class IndicatorForm : Form
    {
        public IndicatorForm(Action onStop)
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            ShowInTaskbar = false;
            BackColor = Color.FromArgb(255, 91, 110);
            Size = new Size(320, 40);
            var wa = Screen.PrimaryScreen.WorkingArea;
            Location = new Point(wa.Right - Width - 16, wa.Top + 16);

            var lbl = new Label
            {
                Text = "● Идёт трансляция экрана оператору",
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 0, 0)
            };
            var stop = new Button
            {
                Text = "Стоп",
                Dock = DockStyle.Right,
                Width = 70,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(200, 50, 66),
                Cursor = Cursors.Hand
            };
            stop.FlatAppearance.BorderSize = 0;
            stop.Click += (s, e) => onStop?.Invoke();

            Controls.Add(lbl);
            Controls.Add(stop);
        }

        // не воруем фокус при показе
        protected override bool ShowWithoutActivation => true;
    }
}
