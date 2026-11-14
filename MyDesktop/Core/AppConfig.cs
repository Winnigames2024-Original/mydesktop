using System;
using System.IO;
using System.Web.Script.Serialization;

namespace MyDesktop.Core
{
    /// <summary>Сохранённая цель быстрого подключения. Pw — пароль в виде DPAPI (не открытый).</summary>
    public class RemotePeer
    {
        public string Login = "";
        public string Pw = "";
        public override string ToString() => Login;
    }

    /// <summary>
    /// Настройки приложения. Хранятся в %AppData%\MyDesktop\config.json.
    /// Открытые пароли здесь не хранятся — только DPAPI-шифртекст для автовхода/быстрого подключения.
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
        public string RustdeskKey = "rF3Fps5QshUgnz9Q0qZuqc8JLlXI+HBjLv5pLJ+eu5k="; // публичный ключ сервера
        public string RustdeskPath = "";              // путь к rustdesk.exe (если пусто — ищем автоматически)

        // Запомненный логин (без пароля)
        public string LastLogin = "";
        public string StreamKey = "";
        public string FfmpegPath = "ffmpeg";   // путь к ffmpeg.exe или просто "ffmpeg" (из PATH)

        // Автовход: пароль хранится локально только в зашифрованном виде (DPAPI)
        public bool AutoLogin = false;
        public string ProtectedPassword = "";

        // Быстрое подключение: сохранённые цели (логин + зашифрованный пароль)
        public System.Collections.Generic.List<RemotePeer> Peers = new System.Collections.Generic.List<RemotePeer>();

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
