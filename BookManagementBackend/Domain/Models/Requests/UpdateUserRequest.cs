using System.ComponentModel.DataAnnotations;

namespace BookManagementBackend.Domain.Models.Requests
{
    public class UpdateUserRequest
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }        
    }
}
