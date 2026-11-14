using System;
using System.IO;
using System.Web.Script.Serialization;

namespace MyDesktop.Core
{
    /// <summary>
    /// Настройки приложения. Хранятся в %AppData%\MyDesktop\config.json.
    /// Пароли здесь НЕ хранятся — только на бэкенде (scrypt).
    /// </summary>
    public class AppConfig
    {
        // VPS MyDesktop (НЕ пересекается с NareLauncher)
        public const string VpsIp = "151.245.217.104";

        // Бэкенд доступен через свой nginx-блок MyDesktop на отдельном порту :2451
        public string BackendBase = "http://" + VpsIp + ":2451/mydesktop";

        // Трансляция: RTMP-пуш напрямую на :1936
        public string RtmpUrl = "rtmp://" + VpsIp + ":1936/live";
        public string HlsBase = "http://" + VpsIp + ":8001/live";

        // RustDesk: свой сигнальный сервер/релей
        public string RustdeskIdServer = VpsIp;       // hbbs :21116
        public int RustdeskIdPort = 21116;
        public string RustdeskRelay = VpsIp + ":21117";
        public string RustdeskKey = "";               // публичный ключ сервера (из id_ed25519.pub)

        // Запомненный логин (без пароля)
        public string LastLogin = "";
        public string StreamKey = "";

        // ───────────────────────── хранение ─────────────────────────
        private static string Dir =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MyDesktop");
        private static string FilePath => Path.Combine(Dir, "config.json");

        public static AppConfig Load()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var json = File.ReadAllText(FilePath);
                    var cfg = new JavaScriptSerializer().Deserialize<AppConfig>(json);
                    if (cfg != null) return cfg;
                }
            }
            catch { /* ignore, вернём дефолт */ }
            return new AppConfig();
        }

        public void Save()
        {
            try
            {
                Directory.CreateDirectory(Dir);
                File.WriteAllText(FilePath, new JavaScriptSerializer().Serialize(this));
            }
            catch { /* не критично */ }
        }
    }
}
