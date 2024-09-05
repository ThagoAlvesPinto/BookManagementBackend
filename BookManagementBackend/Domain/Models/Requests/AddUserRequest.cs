using System.ComponentModel.DataAnnotations;

namespace BookManagementBackend.Domain.Models.Requests
{
    public class AddUserRequest
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsAdministrator { get; set; } = false;
    }
}
