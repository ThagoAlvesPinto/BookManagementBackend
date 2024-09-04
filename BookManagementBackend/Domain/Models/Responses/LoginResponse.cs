namespace BookManagementBackend.Domain.Models.Responses
{
    public class LoginResponse(int id, string email, string firstName, string lastName, bool isAdministrator, string token)
    {
        public int Id { get; set; } = id;
        public string Email { get; set; } = email;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public bool IsAdministrator { get; set; } = isAdministrator;
        public string Token { get; set; } = token;
    }
}
