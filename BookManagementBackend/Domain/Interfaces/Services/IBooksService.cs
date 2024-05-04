using BookManagementBackend.Domain.Models;

namespace BookManagementBackend.Domain.Interfaces.Services
{
    public interface IBooksService
    {
        Task<Books?> GetBookByIsbn(string isbn);
        Task<(bool,string)> ReturnBook(int bookId, string returnUserName);
    }
}
