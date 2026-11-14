using System;
using System.Diagnostics;
using System.IO;

namespace MyDesktop.Core
{
    /// <summary>
    /// «Наш RustDesk»: официальный клиент RustDesk, настроенный на СВОЙ self-hosted
    /// релей (hbbs/hbbr на VPS). Движок управления мы не пишем — берём готовый клиент,
    /// права (мышь/клавиатура, файлы, блокировка ввода, чат, подтверждение, «всегда
    /// разрешать») задаёт сама управляемая сторона в RustDesk.
    /// Настройка идёт через стандартный RustDesk2.toml.
    /// </summary>
    public static class RustDeskManager
    {
        public static string ConfigDir =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RustDesk", "config");
        public static string TomlPath => Path.Combine(ConfigDir, "RustDesk2.toml");

        public static void WriteServerConfig(AppConfig cfg)
        {
            Directory.CreateDirectory(ConfigDir);
            string toml =
                "[options]\n" +
                "custom-rendezvous-server = '" + cfg.RustdeskIdServer + "'\n" +
                "relay-server = '" + cfg.RustdeskIdServer + "'\n" +
                "key = '" + cfg.RustdeskKey + "'\n";
            File.WriteAllText(TomlPath, toml);
        }

        public static string FindClient(AppConfig cfg)
        {
            if (!string.IsNullOrEmpty(cfg.RustdeskPath) && File.Exists(cfg.RustdeskPath)) return cfg.RustdeskPath;
            string[] cands =
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "RustDesk", "rustdesk.exe"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "RustDesk", "rustdesk.exe"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyDesktop", "rustdesk.exe"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rustdesk.exe")
            };
            foreach (var c in cands) if (File.Exists(c)) return c;
            return null;
        }

        /// <summary>Пишет конфиг на наш релей и запускает клиент (опц. сразу к ID).</summary>
        public static bool LaunchConfigured(AppConfig cfg, out string error, string connectId = null)
        {
            error = null;
            try { WriteServerConfig(cfg); }
            catch (Exception ex) { error = "Не удалось записать конфиг RustDesk: " + ex.Message; return false; }

            var exe = FindClient(cfg);
            if (exe == null)
            {
                error = "rustdesk.exe не найден. Нажмите «Скачать RustDesk», установите, либо укажите путь.";
                return false;
            }
            try
            {
                var args = string.IsNullOrEmpty(connectId) ? "" : ("--connect " + connectId);
                Process.Start(new ProcessStartInfo { FileName = exe, Arguments = args, UseShellExecute = true });
                return true;
            }
            catch (Exception ex) { error = ex.Message; return false; }
        }

        public static void OpenDownloadPage()
        {
            try { Process.Start(new ProcessStartInfo { FileName = "https://rustdesk.com/", UseShellExecute = true }); }
            catch { }
        }
    }
}
