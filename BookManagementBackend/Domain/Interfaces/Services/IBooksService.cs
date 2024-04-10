using BookManagementBackend.Domain.Models;

namespace BookManagementBackend.Domain.Interfaces.Services
{
    public interface IBooksService
    {
        Task<Books?> GetBookByIsbn(string isbn);
        Task<bool> ReturnBook(int bookId, string returnUserName);
    }
}
