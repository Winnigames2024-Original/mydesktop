using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace MyDesktop.Core
{
    /// <summary>Параметры трансляции.</summary>
    public class StreamOptions
    {
        public string FfmpegPath = "ffmpeg";
        public int Fps = 30;
        public int BitrateKbps = 2500;
        public Rectangle ScreenBounds;   // область захвата (монитор)
        public string RtmpUrl;           // полный rtmp://.../live/<key>
        public bool CaptureAudio = false;
    }

    /// <summary>
    /// Запуск/остановка ffmpeg для RTMP-пуша экрана (gdigrab → libx264 → flv → RTMP).
    /// Своего «движка» захвата нет — используется внешний ffmpeg, как в OBS.
    /// </summary>
    public class StreamEngine
    {
        private Process _proc;
        public bool Running { get; private set; }

        public event Action<string> Log;
        public event Action<bool> StateChanged;

        public string BuildArgs(StreamOptions o)
        {
            var r = o.ScreenBounds;
            var sb = new StringBuilder();
            sb.Append($"-f gdigrab -framerate {o.Fps} -offset_x {r.X} -offset_y {r.Y} ");
            sb.Append($"-video_size {r.Width}x{r.Height} -i desktop ");
            if (o.CaptureAudio)
                sb.Append("-f dshow -i audio=\"virtual-audio-capturer\" ");
            sb.Append("-c:v libx264 -preset veryfast -tune zerolatency -pix_fmt yuv420p ");
            sb.Append($"-b:v {o.BitrateKbps}k -maxrate {o.BitrateKbps}k -bufsize {o.BitrateKbps * 2}k ");
            sb.Append($"-g {o.Fps * 2} -r {o.Fps} ");
            if (o.CaptureAudio) sb.Append("-c:a aac -b:a 128k -ar 44100 ");
            sb.Append($"-f flv \"{o.RtmpUrl}\"");
            return sb.ToString();
        }

        public void Start(StreamOptions o)
        {
            if (Running) return;
            var psi = new ProcessStartInfo
            {
                FileName = string.IsNullOrWhiteSpace(o.FfmpegPath) ? "ffmpeg" : o.FfmpegPath,
                Arguments = BuildArgs(o),
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                StandardErrorEncoding = Encoding.UTF8
            };
            _proc = new Process { StartInfo = psi, EnableRaisingEvents = true };
            _proc.ErrorDataReceived += (s, e) => { if (e.Data != null) Log?.Invoke(e.Data); };
            _proc.Exited += (s, e) => { Running = false; StateChanged?.Invoke(false); };
            try
            {
                _proc.Start();
                _proc.BeginErrorReadLine();
                Running = true;
                StateChanged?.Invoke(true);
                Log?.Invoke("▶ ffmpeg: " + psi.FileName + " " + psi.Arguments);
            }
            catch (Exception ex)
            {
                Running = false;
                Log?.Invoke("✖ Не удалось запустить ffmpeg: " + ex.Message +
                            "  (укажите путь к ffmpeg.exe, если его нет в PATH)");
                StateChanged?.Invoke(false);
            }
        }

        public void Stop()
        {
            if (!Running || _proc == null) return;
            try
            {
                try { _proc.StandardInput.Write("q"); _proc.StandardInput.Flush(); } catch { }
                if (!_proc.WaitForExit(2500)) { try { _proc.Kill(); } catch { } }
            }
            catch (Exception ex) { Log?.Invoke("Ошибка остановки: " + ex.Message); }
            finally { Running = false; StateChanged?.Invoke(false); }
        }
    }
}
