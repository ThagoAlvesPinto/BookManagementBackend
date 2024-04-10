using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagementBackend.Domain.Models
{
    [Table("BooksReturn")]
    public class BooksReturn(int BookId, DateTime ReturnDate, string ReturnUserName)
    {
        [Key]      
        public int Id { get; }
        public int BookId { get; set; } = BookId;
        public Books? Book { get; set; }
        public DateTime ReturnDate { get; set; } = ReturnDate;
        public string ReturnUserName { get; set; } = ReturnUserName;
        public bool ReturnConfirmed { get; set; } = false;
    }
}
