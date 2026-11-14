using System;
using System.Security.Cryptography;
using System.Text;

namespace MyDesktop.Core
{
    /// <summary>
    /// Локальное шифрование секрета под текущего пользователя Windows (DPAPI).
    /// Используется для «автовхода»: пароль хранится только в зашифрованном виде
    /// и расшифровывается лишь на этой учётке Windows.
    /// </summary>
    public static class Secret
    {
        public static string Protect(string plain)
        {
            if (string.IsNullOrEmpty(plain)) return "";
            try
            {
                var enc = ProtectedData.Protect(Encoding.UTF8.GetBytes(plain), null, DataProtectionScope.CurrentUser);
                return Convert.ToBase64String(enc);
            }
            catch { return ""; }
        }

        public static string Unprotect(string b64)
        {
            if (string.IsNullOrEmpty(b64)) return "";
            try
            {
                var data = ProtectedData.Unprotect(Convert.FromBase64String(b64), null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(data);
            }
            catch { return ""; }
        }
    }
}
