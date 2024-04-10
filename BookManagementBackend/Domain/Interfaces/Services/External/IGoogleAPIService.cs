using BookManagementBackend.Domain.Models.Responses;

namespace BookManagementBackend.Domain.Interfaces.Services.External
{
    public interface IGoogleAPIService
    {
        Task<GoogleBook?> GetGoogleBookByIsbn(string isbn);
    }
}
