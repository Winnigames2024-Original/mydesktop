using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyDesktop.Core.Effects
{
    // ────────────────────────── интерфейс эффекта ──────────────────────────
    public interface IEffect
    {
        string Name { get; }
        bool Active { get; }
        void Start();
        void Stop();
    }

    // ────────────────────────── WinAPI ──────────────────────────
    internal static class Native
    {
        [StructLayout(LayoutKind.Sequential)] public struct POINT { public int X; public int Y; }
        [DllImport("user32.dll")] public static extern bool GetCursorPos(out POINT p);
        [DllImport("user32.dll")] public static extern bool SetCursorPos(int x, int y);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        [DllImport("user32.dll")] public static extern bool EnumWindows(EnumWindowsProc cb, IntPtr p);
        [DllImport("user32.dll")] public static extern bool IsWindowVisible(IntPtr h);
        [DllImport("user32.dll")] public static extern int GetWindowTextLength(IntPtr h);
        [DllImport("user32.dll")] public static extern bool SetWindowPos(IntPtr h, IntPtr after, int x, int y, int cx, int cy, uint flags);
        public const uint SWP_NOSIZE = 0x0001, SWP_NOZORDER = 0x0004, SWP_NOACTIVATE = 0x0010;

        // смена ориентации экрана (переворот)
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string dmDeviceName;
            public short dmSpecVersion, dmDriverVersion, dmSize, dmDriverExtra;
            public int dmFields;
            public int dmPositionX, dmPositionY, dmDisplayOrientation, dmDisplayFixedOutput;
            public short dmColor, dmDuplex, dmYResolution, dmTTOption, dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string dmFormName;
            public short dmLogPixels; public int dmBitsPerPel, dmPelsWidth, dmPelsHeight, dmDisplayFlags, dmDisplayFrequency;
            public int dmICMMethod, dmICMIntent, dmMediaType, dmDitherType, dmReserved1, dmReserved2, dmPanningWidth, dmPanningHeight;
        }
        [DllImport("user32.dll")] public static extern int EnumDisplaySettings(string dev, int mode, ref DEVMODE dm);
        [DllImport("user32.dll")] public static extern int ChangeDisplaySettings(ref DEVMODE dm, int flags);
        public const int ENUM_CURRENT_SETTINGS = -1;
        public const int DM_DISPLAYORIENTATION = 0x00000080;
        public const int DMDO_DEFAULT = 0, DMDO_180 = 2;
    }

    // ────────────────────────── базовый прозрачный оверлей ──────────────────────────
    internal class Overlay : Form
    {
        private readonly bool _clickThrough;
        public Overlay(bool clickThrough)
        {
            _clickThrough = clickThrough;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            ShowInTaskbar = false;
            TopMost = true;
            Bounds = SystemInformation.VirtualScreen;
            BackColor = Color.Black;
            TransparencyKey = Color.Black;
            DoubleBuffered = true;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80000 | 0x08000000 | 0x80; // LAYERED | NOACTIVATE | TOOLWINDOW
                if (_clickThrough) cp.ExStyle |= 0x20;      // TRANSPARENT (клики проходят насквозь)
                return cp;
            }
        }
        protected override bool ShowWithoutActivation => true;
    }

    // ────────────────────────── эффекты ──────────────────────────
    public class CursorShakeEffect : IEffect
    {
        public string Name => "Тряска курсора";
        public bool Active { get; private set; }
        private readonly Timer _t = new Timer { Interval = 25 };
        private readonly Random _r = new Random();
        public CursorShakeEffect() { _t.Tick += (s, e) => { if (Native.GetCursorPos(out var p)) Native.SetCursorPos(p.X + _r.Next(-9, 10), p.Y + _r.Next(-9, 10)); }; }
        public void Start() { Active = true; _t.Start(); }
        public void Stop() { Active = false; _t.Stop(); }
    }

    public class CursorJumpEffect : IEffect
    {
        public string Name => "Прыжки курсора";
        public bool Active { get; private set; }
        private readonly Timer _t = new Timer { Interval = 1400 };
        private readonly Random _r = new Random();
        public CursorJumpEffect()
        {
            _t.Tick += (s, e) =>
            {
                var vs = SystemInformation.VirtualScreen;
                Native.SetCursorPos(vs.Left + _r.Next(vs.Width), vs.Top + _r.Next(vs.Height));
            };
        }
        public void Start() { Active = true; _t.Start(); }
        public void Stop() { Active = false; _t.Stop(); }
    }

    public class LinesEffect : IEffect
    {
        public string Name => "Линии на экране";
        public bool Active { get; private set; }
        private Overlay _o;
        private readonly Timer _t = new Timer { Interval = 350 };
        private readonly List<Tuple<Point, Point, Color>> _lines = new List<Tuple<Point, Point, Color>>();
        private readonly Random _r = new Random();

        public void Start()
        {
            Active = true;
            _o = new Overlay(true);
            _o.Paint += (s, e) =>
            {
                foreach (var l in _lines)
                    using (var pen = new Pen(l.Item3, 2))
                        e.Graphics.DrawLine(pen, l.Item1, l.Item2);
            };
            _o.Show();
            _t.Tick += Tick;
            _t.Start();
        }
        private void Tick(object s, EventArgs e)
        {
            var vs = SystemInformation.VirtualScreen;
            Point P() => new Point(_r.Next(vs.Width), _r.Next(vs.Height));
            _lines.Add(Tuple.Create(P(), P(), Color.FromArgb(_r.Next(120, 256), _r.Next(256), _r.Next(256), _r.Next(256))));
            if (_lines.Count > 60) _lines.RemoveAt(0);
            _o?.Invalidate();
        }
        public void Stop()
        {
            Active = false; _t.Stop(); _t.Tick -= Tick; _lines.Clear();
            try { _o?.Close(); _o?.Dispose(); } catch { } _o = null;
        }
    }

    public class FogEffect : IEffect   // «расплывание» — полупрозрачная дымка
    {
        public string Name => "Расплывание";
        public bool Active { get; private set; }
        private Form _o;
        public void Start()
        {
            Active = true;
            _o = new Overlay(true) { BackColor = Color.White, TransparencyKey = Color.Empty, Opacity = 0.45 };
            _o.Show();
        }
        public void Stop() { Active = false; try { _o?.Close(); _o?.Dispose(); } catch { } _o = null; }
    }

    public class TrainEffect : IEffect   // «паровозик» едет по экрану
    {
        public string Name => "Паровозик";
        public bool Active { get; private set; }
        private Overlay _o;
        private Label _lbl;
        private readonly Timer _t = new Timer { Interval = 30 };
        private int _x;
        public void Start()
        {
            Active = true;
            var vs = SystemInformation.VirtualScreen;
            _o = new Overlay(true);
            _lbl = new Label { Text = "🚂💨", Font = new Font("Segoe UI Emoji", 42f), AutoSize = true, BackColor = Color.Black, ForeColor = Color.White };
            _o.Controls.Add(_lbl);
            _o.Show();
            _x = -120;
            _t.Tick += (s, e) =>
            {
                _x += 8; if (_x > vs.Width) _x = -120;
                _lbl.Location = new Point(_x, vs.Height / 3);
            };
            _t.Start();
        }
        public void Stop() { Active = false; _t.Stop(); try { _o?.Close(); _o?.Dispose(); } catch { } _o = null; }
    }

    public class WindowsJumpEffect : IEffect   // окна прыгают в random
    {
        public string Name => "Прыгающие окна";
        public bool Active { get; private set; }
        private readonly Timer _t = new Timer { Interval = 1200 };
        private readonly Random _r = new Random();
        public WindowsJumpEffect() { _t.Tick += (s, e) => Jump(); }
        private void Jump()
        {
            var vs = SystemInformation.VirtualScreen;
            IntPtr self = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            Native.EnumWindows((h, l) =>
            {
                if (h == self) return true;
                if (!Native.IsWindowVisible(h)) return true;
                if (Native.GetWindowTextLength(h) == 0) return true;
                Native.SetWindowPos(h, IntPtr.Zero, vs.Left + _r.Next(Math.Max(1, vs.Width - 200)),
                    vs.Top + _r.Next(Math.Max(1, vs.Height - 150)), 0, 0,
                    Native.SWP_NOSIZE | Native.SWP_NOZORDER | Native.SWP_NOACTIVATE);
                return true;
            }, IntPtr.Zero);
        }
        public void Start() { Active = true; _t.Start(); }
        public void Stop() { Active = false; _t.Stop(); }
    }

    public class FlipScreenEffect : IEffect   // переворот экрана (с авто-возвратом)
    {
        public string Name => "Переворот экрана";
        public bool Active { get; private set; }
        private readonly Timer _auto = new Timer { Interval = 8000 };
        public FlipScreenEffect() { _auto.Tick += (s, e) => Stop(); }

        private void SetOrientation(int orient)
        {
            var dm = new Native.DEVMODE(); dm.dmSize = (short)Marshal.SizeOf(typeof(Native.DEVMODE));
            if (Native.EnumDisplaySettings(null, Native.ENUM_CURRENT_SETTINGS, ref dm) == 0) return;
            // для 180° размеры не меняем
            dm.dmFields |= Native.DM_DISPLAYORIENTATION;
            dm.dmDisplayOrientation = orient;
            Native.ChangeDisplaySettings(ref dm, 0);
        }
        public void Start() { Active = true; try { SetOrientation(Native.DMDO_180); } catch { } _auto.Start(); }
        public void Stop() { if (!Active) return; Active = false; _auto.Stop(); try { SetOrientation(Native.DMDO_DEFAULT); } catch { } }
    }

    public class BsodJokeEffect : IEffect   // «синий экран»-ШУТКА с крестиком закрытия
    {
        public string Name => "«Синий экран» (шутка)";
        public bool Active { get; private set; }
        private Form _f;
        public void Start()
        {
            Active = true;
            _f = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                WindowState = FormWindowState.Maximized,
                TopMost = true,
                BackColor = Color.FromArgb(0, 120, 215),
                StartPosition = FormStartPosition.Manual,
                Bounds = SystemInformation.VirtualScreen,
                ShowInTaskbar = false
            };
            var lbl = new Label
            {
                Text = ":(\n\nЭто просто шутка. Нажмите ✕ в правом верхнем углу, чтобы закрыть.",
                ForeColor = Color.White, Font = new Font("Segoe UI", 28f), AutoSize = false,
                Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter
            };
            var close = new Button
            {
                Text = "✕", ForeColor = Color.White, BackColor = Color.FromArgb(0, 99, 177),
                FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                Size = new Size(48, 40), Location = new Point(_f.Width - 60, 12), Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            close.FlatAppearance.BorderSize = 0;
            close.Click += (s, e) => Stop();
            _f.Controls.Add(close); _f.Controls.Add(lbl);
            close.BringToFront();
            _f.Show();
        }
        public void Stop() { Active = false; try { _f?.Close(); _f?.Dispose(); } catch { } _f = null; }
    }

    public class ShowImageEffect : IEffect   // показать картинку на весь экран
    {
        public string Name => "Картинка на экран";
        public bool Active { get; private set; }
        private Form _f;
        public void Start()
        {
            string path = null;
            using (var ofd = new OpenFileDialog { Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp;*.gif" })
                if (ofd.ShowDialog() == DialogResult.OK) path = ofd.FileName;
            if (path == null) { Active = false; return; }

            Active = true;
            _f = new Form
            {
                FormBorderStyle = FormBorderStyle.None, TopMost = true,
                StartPosition = FormStartPosition.Manual, Bounds = SystemInformation.VirtualScreen,
                BackColor = Color.Black, ShowInTaskbar = false
            };
            var pb = new PictureBox { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom };
            try { pb.Image = Image.FromFile(path); } catch { }
            var close = new Button
            {
                Text = "✕", ForeColor = Color.White, BackColor = Color.FromArgb(40, 40, 40),
                FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                Size = new Size(44, 36), Location = new Point(_f.Width - 56, 12),
                Anchor = AnchorStyles.Top | AnchorStyles.Right, Cursor = Cursors.Hand
            };
            close.FlatAppearance.BorderSize = 0;
            close.Click += (s, e) => Stop();
            _f.Controls.Add(close); _f.Controls.Add(pb); close.BringToFront();
            _f.Show();
        }
        public void Stop() { Active = false; try { _f?.Close(); _f?.Dispose(); } catch { } _f = null; }
    }
}
