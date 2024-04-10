using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagementBackend.Domain.Models
{
    [Table("Users")]
    public class Users(string UserName, string Password, string Email)
    {
        [Key]
        public int Id { get; }
        public string UserName { get; set; } = UserName;
        public string Password { get; set; } = Password;
        public string Email { get; set; } = Email;
        public bool IsAdministrator { get; set; } = false;
    }
}
