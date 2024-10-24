using System.ComponentModel.DataAnnotations;

namespace BookManagementBackend.Domain.Models.Requests
{
    public class UpdatePasswordRequest
    {
        [Required]
        public required string OldPassword { get; set; }

        [Required]
        public required string NewPassword { get; set; }
    }
}
