using BookManagementBackend.Classes;
using BookManagementBackend.Domain.Models;
using BookManagementBackend.Domain.Models.Responses;

namespace BookManagementBackend.Domain.Interfaces.Services
{
    public interface IUsersService
    {
        Task<ServiceResult<List<UserResponse>>> GetAllUsers();
        Task<ServiceResult<Users>> Login(string email, string password);
    }
}
