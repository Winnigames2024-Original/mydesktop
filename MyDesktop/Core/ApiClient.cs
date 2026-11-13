using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MyDesktop.Core
{
    /// <summary>Результат вызова API.</summary>
    public class ApiResult
    {
        public bool Ok;
        public string Message;
        public Dictionary<string, object> Data;
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
    /// Без сторонних зависимостей: JSON через встроенный JavaScriptSerializer.
    /// Пароль локально не сохраняется.
    /// </summary>
    public class ApiClient
    {
        private readonly HttpClient _http;
        private readonly string _base;
        private readonly JavaScriptSerializer _js = new JavaScriptSerializer();

        public ApiClient(AppConfig cfg)
        {
            _base = cfg.BackendBase.TrimEnd('/');
            _http = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
        }

        private Dictionary<string, object> AsDict(string text)
        {
            try { return _js.DeserializeObject(text) as Dictionary<string, object>; }
            catch { return null; }
        }

        private async Task<ApiResult> PostJson(string path, object body)
        {
            try
            {
                var json = _js.Serialize(body);
                var resp = await _http.PostAsync(_base + path,
                    new StringContent(json, Encoding.UTF8, "application/json")).ConfigureAwait(false);
                var text = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                var jo = AsDict(text);
                if (resp.IsSuccessStatusCode)
                    return new ApiResult { Ok = true, Data = jo };

                string msg = null;
                if (jo != null)
                {
                    object v;
                    if (jo.TryGetValue("message", out v) && v != null) msg = v.ToString();
                    else if (jo.TryGetValue("error", out v) && v != null) msg = v.ToString();
                }
                return new ApiResult { Ok = false, Message = msg ?? ("HTTP " + (int)resp.StatusCode), Data = jo };
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
                var jo = AsDict(text);
                object c;
                if (jo != null && jo.TryGetValue("count", out c) && c != null)
                    return Convert.ToInt32(c);
                return 0;
            }
            catch { return -1; } // -1 = сервер недоступен
        }

        public async Task<UpdateInfo> GetUpdateInfoAsync()
        {
            try
            {
                var text = await _http.GetStringAsync(_base + "/update.json").ConfigureAwait(false);
                return _js.Deserialize<UpdateInfo>(text);
            }
            catch { return null; }
        }
    }
}
