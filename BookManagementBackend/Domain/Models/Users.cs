using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagementBackend.Domain.Models
{
    [Table("Users")]
    public class Users(string email, string firstName, string lastName)
    {
        [Key]
        public int Id { get; }
        public string? Password { get; set; };
        public string Email { get; set; } = email;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public bool IsAdministrator { get; set; } = false;
    }
}
