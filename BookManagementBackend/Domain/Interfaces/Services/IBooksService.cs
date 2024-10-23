using BookManagementBackend.Classes;
using BookManagementBackend.Domain.Models;
using BookManagementBackend.Domain.Models.Requests;
using BookManagementBackend.Domain.Models.Responses;

namespace BookManagementBackend.Domain.Interfaces.Services
{
    public interface IBooksService
    {
        Task<ServiceResult> AddBook(AddBookRequest bookReq);
        Task<ServiceResult> DeleteBook(int bookId);
        Task<ServiceResult<Books>> GetBookByIsbn(string isbn);
        Task<ServiceResult<List<Books>>> GetBooks();
        Task<ServiceResult<ExternalBookResponse>> GetExternalBook(string isbn);
        Task<ServiceResult> ReturnBook(int bookId, int userId);
        Task<ServiceResult> UpdateBook(UpdateBookRequest bookReq);
    }
}
