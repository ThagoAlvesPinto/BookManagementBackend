using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagementBackend.Domain.Models
{
    [Table("BooksReturn")]
    public class BooksReturn()
    {
        [Key]      
        public int Id { get; }
        public int BookId { get; set; }        
        public DateTime ReturnDate { get; set; }
        public bool ReturnConfirmed { get; set; } = false;
        public int ReturnUserId { get; set; }

        public Users? ReturnUser { get; set; }
        public Books? Book { get; set; }
    }
}
