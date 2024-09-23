namespace BookManagementBackend.Domain.Models.Responses
{
    public class UserResponse
    {
        public UserResponse(Users user)
        {
            Id = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            IsAdministrator = user.IsAdministrator;
            Active = user.Active;
        }

        public int Id { get; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsAdministrator { get; set; } = false;
        public bool Active { get; set; } = true;
    }
}
