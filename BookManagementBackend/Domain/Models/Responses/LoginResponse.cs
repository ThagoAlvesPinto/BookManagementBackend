namespace BookManagementBackend.Domain.Models.Responses
{
    public class LoginResponse(Users user, string token)
    {
        public Users User { get; set; } = user;
        public string Token { get; set; } = token;
    }
}
