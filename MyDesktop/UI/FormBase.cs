using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyDesktop.UI
{
    /// <summary>
    /// Безрамочная тёмная форма: скруглённые углы, перетаскивание за верх,
    /// кнопки свернуть/закрыть, плавное появление (fade-in).
    /// </summary>
    public class FormBase : Form
    {
        protected int CornerRadius = 18;
        protected int TitleBarHeight = 44;
        protected string TitleText = "MyDesktop";
        protected bool ShowMinimize = true;

        private readonly Timer _fade = new Timer { Interval = 15 };

        public FormBase()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Theme.Bg;
            ForeColor = Theme.Text;
            Font = Theme.FontBody;
            DoubleBuffered = true;
            Opacity = 0;
            SetStyle(ControlStyles.ResizeRedraw, true);

            Load += (s, e) => _fade.Start();
            _fade.Tick += (s, e) =>
            {
                if (Opacity < 1) Opacity = Math.Min(1, Opacity + 0.12);
                else _fade.Stop();
            };
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ApplyRoundRegion();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyRoundRegion();
        }

        private void ApplyRoundRegion()
        {
            using (var path = Theme.RoundRect(new Rectangle(0, 0, Width, Height), CornerRadius))
                Region = new Region(path);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // верхняя полоса-заголовок
            using (var b = new SolidBrush(Theme.Surface))
                g.FillRectangle(b, 0, 0, Width, TitleBarHeight);

            // логотип-маркер
            var markRect = new Rectangle(16, (TitleBarHeight - 22) / 2, 22, 22);
            using (var path = Theme.RoundRect(markRect, 7))
            using (var ab = Theme.AccentBrush(markRect))
                g.FillPath(ab, path);

            TextRenderer.DrawText(g, TitleText, Theme.FontH2,
                new Rectangle(48, 0, Width - 160, TitleBarHeight), Theme.Text,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

            // кнопки окна
            DrawWinButton(g, _closeRect, Theme.Danger, "✕");
            if (ShowMinimize) DrawWinButton(g, _minRect, Theme.Muted, "—");

            using (var pen = new Pen(Theme.Line))
                g.DrawLine(pen, 0, TitleBarHeight, Width, TitleBarHeight);
        }

        private void DrawWinButton(Graphics g, Rectangle r, Color c, string glyph)
        {
            TextRenderer.DrawText(g, glyph, new Font("Segoe UI", 10f, FontStyle.Bold), r, c,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private Rectangle _closeRect => new Rectangle(Width - 40, (TitleBarHeight - 28) / 2, 28, 28);
        private Rectangle _minRect   => new Rectangle(Width - 76, (TitleBarHeight - 28) / 2, 28, 28);

        // ── перетаскивание + кнопки ──
        [DllImport("user32.dll")] private static extern int SendMessage(IntPtr h, int m, int w, int l);
        [DllImport("user32.dll")] private static extern bool ReleaseCapture();

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left) return;
            if (_closeRect.Contains(e.Location)) { Close(); return; }
            if (ShowMinimize && _minRect.Contains(e.Location)) { WindowState = FormWindowState.Minimized; return; }
            if (e.Y <= TitleBarHeight)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1 /*WM_NCLBUTTONDOWN*/, 0x2 /*HTCAPTION*/, 0);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Cursor = (_closeRect.Contains(e.Location) || (ShowMinimize && _minRect.Contains(e.Location)))
                ? Cursors.Hand : Cursors.Default;
        }
    }
}
