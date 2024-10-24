using BookManagementBackend.Classes;
using BookManagementBackend.Domain.Models;
using BookManagementBackend.Domain.Models.Requests;
using BookManagementBackend.Domain.Models.Responses;

namespace BookManagementBackend.Domain.Interfaces.Services
{
    public interface IUsersService
    {
        Task<ServiceResult> AddUser(AddUserRequest userRequest);
        Task<ServiceResult> DeactivateUser(int userId);
        Task<ServiceResult<List<UserResponse>>> GetAllUsers();
        Task<ServiceResult<UserResponse>> GetUser(int userId);
        Task<ServiceResult<Users>> Login(string email, string password);
        Task<ServiceResult> UpdatePassword(UpdatePasswordRequest request, int userId);
        Task<ServiceResult> UpdateUser(UpdateUserRequest request, int userId);
    }
}
