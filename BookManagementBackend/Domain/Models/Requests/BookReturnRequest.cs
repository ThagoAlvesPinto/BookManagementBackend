using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookManagementBackend.Domain.Models.Requests
{
    public class BookReturnRequest
    {
        [Required]
        [JsonPropertyName("bookId")]
        public int BookId { get; set; }

        [Required]
        [JsonPropertyName("userName")]
        public string ReturnUserName { get; set; }
    }
}
