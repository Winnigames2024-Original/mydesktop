using System;
using System.Diagnostics;
using System.IO;

namespace MyDesktop.Core
{
    /// <summary>
    /// Интеграция официального клиента RustDesk с НАШИМ self-hosted релеем.
    /// Мы НЕ пишем движок управления — только настраиваем и запускаем официальный клиент.
    /// Настройка идёт через стандартный файл конфигурации RustDesk2.toml.
    /// </summary>
    public static class RustDeskManager
    {
        public static string ConfigDir =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RustDesk", "config");
        public static string TomlPath => Path.Combine(ConfigDir, "RustDesk2.toml");

        /// <summary>Прописывает наш сигнальный/релейный сервер и ключ в конфиг клиента.</summary>
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

        /// <summary>
        /// Пишет конфиг на наш релей и запускает клиент.
        /// connectId != null → сразу подключиться к устройству по его ID (режим оператора).
        /// </summary>
        public static bool LaunchConfigured(AppConfig cfg, out string error, string connectId = null)
        {
            error = null;
            try { WriteServerConfig(cfg); }
            catch (Exception ex) { error = "Не удалось записать конфиг RustDesk: " + ex.Message; return false; }

            var exe = FindClient(cfg);
            if (exe == null)
            {
                error = "rustdesk.exe не найден. Нажмите «Скачать RustDesk» и установите, либо укажите путь к exe.";
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

        public static void OpenUrl(string url)
        {
            try { Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true }); }
            catch { }
        }
    }
}
