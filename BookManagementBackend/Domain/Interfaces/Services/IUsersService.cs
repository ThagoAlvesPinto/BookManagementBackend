using BookManagementBackend.Classes;
using BookManagementBackend.Domain.Models;

namespace BookManagementBackend.Domain.Interfaces.Services
{
    public interface IUsersService
    {
        Task<ServiceResult<IEnumerable<Users>>> GetAllUsers();
        Task<ServiceResult<Users>> Login(string email, string password);
    }
}
