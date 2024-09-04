using System.ComponentModel.DataAnnotations;

namespace BookManagementBackend.Domain.Models.Requests
{
    public class AddBookRequest
    {
        [Required]
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public string? PublishedDate { get; set; }
        public int? Pages { get; set; }
        public string? Language { get; set; }
        public string? Description { get; set; }
        public string? Publisher { get; set; }
        public string? ImageLink { get; set; }
        [Required]
        public string? Isbn10 { get; set; }
        [Required]
        public string? Isbn13 { get; set; }
    }
}
