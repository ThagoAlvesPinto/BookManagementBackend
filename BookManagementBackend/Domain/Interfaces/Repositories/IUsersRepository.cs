using BookManagementBackend.Domain.Models;

namespace BookManagementBackend.Domain.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task AddUser(Users user);
        Task<IEnumerable<Users>> GetAllUsers();
        Task<Users?> GetUser(string email);
        Task<Users?> GetUser(int id);
        Task UpdateUser(Users user);
    }
}
