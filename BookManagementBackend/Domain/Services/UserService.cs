using BookManagementBackend.Domain.Interfaces.Services;

namespace BookManagementBackend.Domain.Services
{
    public class UserService : IUserService
    {
        public Task<bool> IsAuthenticated(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
