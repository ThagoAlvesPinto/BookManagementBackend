using System.Globalization;

namespace BookManagementBackend.Classes
{
    public class AppSettings
    {
        public string JWTSecret { get; set; } = string.Empty;
        public string SendGridKey { get; set; } = string.Empty;
    }
}
