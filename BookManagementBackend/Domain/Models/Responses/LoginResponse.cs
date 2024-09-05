namespace BookManagementBackend.Domain.Models.Responses
{
    public class LoginResponse
    {
        public int Id { get; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsAdministrator { get; set; }
        public bool Active { get; set; }
        public string Token { get; set; }

        public LoginResponse(Users user, string token)
        {
            Token = token;
            Id = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            IsAdministrator = user.IsAdministrator;
            Active = user.Active;
        }
    }
}
