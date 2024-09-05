using System.Globalization;

namespace BookManagementBackend.Classes
{
    public class AppSettings
    {
        public string JWTSecret { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string SMTPServer { get; set; } = string.Empty;
        public int SMTPPort { get; set; } = 0;
        public bool EnableSSL { get; set; } = false;
        public string EmailDisplayName { get; set; } = string.Empty;
    }
}
