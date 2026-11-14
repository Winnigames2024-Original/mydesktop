using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MyDesktop.Core
{
    /// <summary>Автозапуск приложения при входе в Windows (ключ HKCU\...\Run).</summary>
    public static class Startup
    {
        private const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private const string ValueName = "MyDesktop";

        public static bool IsEnabled()
        {
            try { using (var k = Registry.CurrentUser.OpenSubKey(RunKey)) return k?.GetValue(ValueName) != null; }
            catch { return false; }
        }

        public static void Set(bool enable)
        {
            try
            {
                using (var k = Registry.CurrentUser.OpenSubKey(RunKey, true) ?? Registry.CurrentUser.CreateSubKey(RunKey))
                {
                    if (k == null) return;
                    if (enable) k.SetValue(ValueName, "\"" + Application.ExecutablePath + "\"");
                    else if (k.GetValue(ValueName) != null) k.DeleteValue(ValueName, false);
                }
            }
            catch { /* не критично */ }
        }
    }
}
