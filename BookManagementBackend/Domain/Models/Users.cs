using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookManagementBackend.Domain.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int Id { get; }
        [JsonIgnore]
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsAdministrator { get; set; } = false;
        public bool Active { get; set; } = true;

        public ICollection<BooksReturn>? BooksReturn { get; set; }
    }
}
