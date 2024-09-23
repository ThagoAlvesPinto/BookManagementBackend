using BookManagementBackend.Domain.Models;

namespace BookManagementBackend.Domain.Interfaces.Repositories
{
    public interface IBooksRepository
    {
        Task AddBook(Books book);
        Task<bool> BookExists(string isbn);
        Task DeleteBook(Books book);
        Task<List<Books>> GetAllBooks();
        Task<Books?> GetBook(int id);
        Task<Books?> GetBook(string isbn);
        Task UpdateBook(Books book);
    }
}
