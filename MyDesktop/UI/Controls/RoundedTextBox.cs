using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyDesktop.UI.Controls
{
    /// <summary>Поле ввода на скруглённой тёмной подложке с подсветкой фокуса.</summary>
    public class RoundedTextBox : Panel
    {
        public TextBox Inner { get; }
        public int Radius { get; set; } = 12;
        private bool _focused;

        public string Placeholder { get; set; } = "";
        public bool UseSystemPasswordChar { get => Inner.UseSystemPasswordChar; set => Inner.UseSystemPasswordChar = value; }
        public override string Text { get => Inner.Text; set => Inner.Text = value; }

        public RoundedTextBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Theme.Surface;
            Height = 44;
            Padding = new Padding(14, 0, 14, 0);
            Inner = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = Theme.Surface2,
                ForeColor = Theme.Text,
                Font = new Font("Segoe UI", 11f),
                Dock = DockStyle.Fill
            };
            var host = new Panel { Dock = DockStyle.Fill, BackColor = Theme.Surface2, Padding = new Padding(0, 12, 0, 0) };
            host.Controls.Add(Inner);
            Controls.Add(host);
            Inner.GotFocus += (s, e) => { _focused = true; Invalidate(); };
            Inner.LostFocus += (s, e) => { _focused = false; Invalidate(); };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent?.BackColor ?? Theme.Surface);
            var r = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = Theme.RoundRect(r, Radius))
            {
                using (var b = new SolidBrush(Theme.Surface2)) g.FillPath(b, path);
                using (var pen = new Pen(_focused ? Theme.Accent : Theme.Line, _focused ? 1.6f : 1f)) g.DrawPath(pen, path);
            }
        }
    }
}
