using System.Globalization;

namespace BookManagementBackend.Classes
{
    public class AppSettings
    {
        public string JWTSecret { get; set; } = string.Empty;

        public string AdminUsername { get; set; } = string.Empty;
        public string AdminPassword { get; set; } = string.Empty;
    }
}
