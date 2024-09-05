using BookManagementBackend.Domain.Models;

namespace BookManagementBackend.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(Users user);
    }
}
