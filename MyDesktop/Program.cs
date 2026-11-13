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

            using (var login = new LoginForm(cfg))
            {
                if (login.ShowDialog() != DialogResult.OK || !Session.LoggedIn)
                    return; // не вошли — выходим

                Application.Run(new MainForm(cfg));
            }
        }
    }
}
