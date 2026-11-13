using System.Drawing;
using System.Drawing.Drawing2D;

namespace MyDesktop.UI
{
    /// <summary>Единая тёмная палитра и помощники рисования со скруглениями.</summary>
    public static class Theme
    {
        public static readonly Color Bg       = Color.FromArgb(13, 15, 20);   // #0d0f14
        public static readonly Color Surface  = Color.FromArgb(22, 26, 34);   // #161a22
        public static readonly Color Surface2 = Color.FromArgb(30, 36, 48);   // #1e2430
        public static readonly Color Line     = Color.FromArgb(38, 45, 58);   // #262d3a
        public static readonly Color Text      = Color.FromArgb(232, 236, 243); // #e8ecf3
        public static readonly Color Muted     = Color.FromArgb(138, 147, 166); // #8a93a6
        public static readonly Color Accent    = Color.FromArgb(91, 140, 255);  // #5b8cff
        public static readonly Color Accent2   = Color.FromArgb(124, 92, 255);  // #7c5cff
        public static readonly Color Danger     = Color.FromArgb(255, 91, 110); // #ff5b6e
        public static readonly Color Ok         = Color.FromArgb(61, 220, 132); // #3ddc84

        public static Font FontH1  => new Font("Segoe UI Semibold", 20f, FontStyle.Bold);
        public static Font FontH2  => new Font("Segoe UI Semibold", 13f, FontStyle.Bold);
        public static Font FontBody => new Font("Segoe UI", 10f);
        public static Font FontSmall => new Font("Segoe UI", 8.5f);

        /// <summary>Прямоугольник со скруглёнными углами.</summary>
        public static GraphicsPath RoundRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            var p = new GraphicsPath();
            if (d <= 0) { p.AddRectangle(r); p.CloseFigure(); return p; }
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        public static LinearGradientBrush AccentBrush(Rectangle r)
            => new LinearGradientBrush(r, Accent, Accent2, LinearGradientMode.ForwardDiagonal);

        public static LinearGradientBrush DangerBrush(Rectangle r)
            => new LinearGradientBrush(r, Danger, Color.FromArgb(255, 143, 91), LinearGradientMode.ForwardDiagonal);
    }
}
