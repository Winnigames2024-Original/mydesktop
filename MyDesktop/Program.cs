using System;
using System.Windows.Forms;
using MyDesktop.Core;
using MyDesktop.UI;

namespace MyDesktop
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Net.ServicePointManager.SecurityProtocol =
                System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11;

            var cfg = AppConfig.Load();

            // автовход: если включён и есть сохранённый (зашифрованный) пароль — входим без формы
            if (cfg.AutoLogin && !string.IsNullOrEmpty(cfg.LastLogin) && !string.IsNullOrEmpty(cfg.ProtectedPassword)
                && TryAutoLogin(cfg))
            {
                Application.Run(new MainForm(cfg));
                return;
            }

            using (var login = new LoginForm(cfg))
            {
                if (login.ShowDialog() != DialogResult.OK || !Session.LoggedIn)
                    return; // не вошли — выходим

                Application.Run(new MainForm(cfg));
            }
        }

        private static bool TryAutoLogin(AppConfig cfg)
        {
            try
            {
                var pass = Secret.Unprotect(cfg.ProtectedPassword);
                if (string.IsNullOrEmpty(pass)) return false;
                var api = new ApiClient(cfg);
                var r = api.LoginAsync(cfg.LastLogin, pass).GetAwaiter().GetResult();
                if (!r.Ok) return false;
                bool admin = false;
                if (r.Data != null && r.Data.TryGetValue("account", out var accObj)
                    && accObj is System.Collections.Generic.Dictionary<string, object> acc
                    && acc.TryGetValue("admin", out var av) && av is bool bv) admin = bv;
                Session.Set(cfg.LastLogin, admin);
                return true;
            }
            catch { return false; }
        }
    }
}
