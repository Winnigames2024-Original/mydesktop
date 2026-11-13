using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyDesktop.Core
{
    /// <summary>Результат вызова API.</summary>
    public class ApiResult
    {
        public bool Ok;
        public string Message;
        public JObject Data;
        public static ApiResult Fail(string m) => new ApiResult { Ok = false, Message = m };
    }

    public class UpdateInfo
    {
        public string version;
        public string sha1;
        public long size;
        public string url;
    }

    /// <summary>
    /// Клиент бэкенда MyDesktop (:2450 через nginx /mydesktop/).
    /// Пароли отправляются по HTTPS/HTTP на бэкенд, где хешируются scrypt.
    /// Локально пароль нигде не сохраняется.
    /// </summary>
    public class ApiClient
    {
        private readonly HttpClient _http;
        private readonly string _base;

        public ApiClient(AppConfig cfg)
        {
            _base = cfg.BackendBase.TrimEnd('/');
            _http = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
        }

        private async Task<ApiResult> PostJson(string path, object body)
        {
            try
            {
                var json = JsonConvert.SerializeObject(body);
                var resp = await _http.PostAsync(_base + path,
                    new StringContent(json, Encoding.UTF8, "application/json")).ConfigureAwait(false);
                var text = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                JObject jo = null;
                try { jo = JObject.Parse(text); } catch { }
                if (resp.IsSuccessStatusCode)
                    return new ApiResult { Ok = true, Data = jo };
                var msg = jo?["message"]?.ToString() ?? jo?["error"]?.ToString() ?? ("HTTP " + (int)resp.StatusCode);
                return new ApiResult { Ok = false, Message = msg, Data = jo };
            }
            catch (Exception e)
            {
                return ApiResult.Fail("Нет связи с сервером: " + e.Message);
            }
        }

        public Task<ApiResult> RegisterAsync(string login, string password)
            => PostJson("/api/register", new { login, password });

        public Task<ApiResult> LoginAsync(string login, string password)
            => PostJson("/api/login", new { login, password });

        public Task<ApiResult> DeleteAsync(string login, string password)
            => PostJson("/api/delete", new { login, password });

        public async Task<int> CountAsync()
        {
            try
            {
                var text = await _http.GetStringAsync(_base + "/api/count").ConfigureAwait(false);
                return JObject.Parse(text)["count"]?.Value<int>() ?? 0;
            }
            catch { return -1; } // -1 = сервер недоступен
        }

        public async Task<UpdateInfo> GetUpdateInfoAsync()
        {
            try
            {
                var text = await _http.GetStringAsync(_base + "/update.json").ConfigureAwait(false);
                return JsonConvert.DeserializeObject<UpdateInfo>(text);
            }
            catch { return null; }
        }
    }
}
