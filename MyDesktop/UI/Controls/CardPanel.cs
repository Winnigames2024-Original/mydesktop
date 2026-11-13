using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyDesktop.UI.Controls
{
    /// <summary>Скруглённая панель-«карточка» с тенью-рамкой.</summary>
    public class CardPanel : Panel
    {
        public int Radius { get; set; } = 16;
        public Color Fill { get; set; } = Theme.Surface;
        public bool Border { get; set; } = true;

        public CardPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Theme.Bg;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent?.BackColor ?? Theme.Bg);
            var r = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = Theme.RoundRect(r, Radius))
            {
                using (var b = new SolidBrush(Fill)) g.FillPath(b, path);
                if (Border) using (var pen = new Pen(Theme.Line)) g.DrawPath(pen, path);
            }
        }
    }
}
