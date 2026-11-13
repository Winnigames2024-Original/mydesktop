using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyDesktop.UI.Controls
{
    /// <summary>Кнопка со скруглением, градиентом-акцентом и плавным ховером.</summary>
    public class RoundedButton : Button
    {
        public int Radius { get; set; } = 12;
        public bool Danger { get; set; } = false;
        public bool Ghost { get; set; } = false;   // прозрачная кнопка с рамкой (для навигации)
        public bool Active { get; set; } = false;   // подсветка активного пункта навигации

        private float _hover = 0f;
        private readonly Timer _anim = new Timer { Interval = 15 };
        private bool _hovering;

        public RoundedButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Theme.Surface;
            ForeColor = Theme.Text;
            Font = new Font("Segoe UI Semibold", 10f, FontStyle.Bold);
            Cursor = Cursors.Hand;
            Height = 42;
            _anim.Tick += (s, e) =>
            {
                float target = _hovering ? 1f : 0f;
                _hover += (target - _hover) * 0.25f;
                if (Math.Abs(target - _hover) < 0.01f) { _hover = target; _anim.Stop(); }
                Invalidate();
            };
        }

        protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); _hovering = true; _anim.Start(); }
        protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); _hovering = false; _anim.Start(); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent?.BackColor ?? Theme.Surface);
            var r = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = Theme.RoundRect(r, Radius))
            {
                if (Ghost)
                {
                    if (Active)
                        using (var b = new SolidBrush(Theme.Surface2)) g.FillPath(b, path);
                    using (var pen = new Pen(Active ? Theme.Accent : Theme.Line, Active ? 1.5f : 1f)) g.DrawPath(pen, path);
                }
                else
                {
                    using (var baseBrush = Danger ? Theme.DangerBrush(r) : Theme.AccentBrush(r))
                        g.FillPath(baseBrush, path);
                    // ховер-подсветка сверху
                    if (_hover > 0.01f)
                        using (var hb = new SolidBrush(Color.FromArgb((int)(40 * _hover), 255, 255, 255)))
                            g.FillPath(hb, path);
                }

                var textColor = Ghost ? (Active ? Theme.Text : Theme.Muted) : Color.White;
                TextRenderer.DrawText(g, Text, Font, r, textColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            }
        }
    }
}
