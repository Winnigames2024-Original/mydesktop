namespace MyDesktop.Core
{
    /// <summary>Текущий вошедший пользователь (на время работы приложения).</summary>
    public static class Session
    {
        public static string Login;
        public static bool IsAdmin;
        public static bool LoggedIn => !string.IsNullOrEmpty(Login);

        public static void Set(string login, bool admin)
        {
            Login = login;
            IsAdmin = admin;
        }

        public static void Clear()
        {
            Login = null;
            IsAdmin = false;
        }
    }
}
