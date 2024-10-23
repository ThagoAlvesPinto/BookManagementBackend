using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookManagementBackend.Domain.Models.Requests
{
    public class UpdateBookRequest
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public string? PublishedDate { get; set; }
        public int? Pages { get; set; }
        public string? Language { get; set; }
        public string? Description { get; set; }
        public string? Publisher { get; set; }
        public string? ImageLink { get; set; }
        public string? Isbn10 { get; set; }
        public string? Isbn13 { get; set; }
    }
}
