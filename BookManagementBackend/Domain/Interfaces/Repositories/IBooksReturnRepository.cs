using BookManagementBackend.Domain.Models;

namespace BookManagementBackend.Domain.Interfaces.Repositories
{
    public interface IBooksReturnRepository
    {
        Task AddBookReturn(BooksReturn bookReturn);
        Task DeleteBookReturn(BooksReturn bookReturn);
        Task<List<BooksReturn>> GetAllBooksReturnByBook(int bookId);
        Task<BooksReturn?> GetBookReturn(int id);
    }
}
