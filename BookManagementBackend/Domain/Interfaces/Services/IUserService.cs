namespace BookManagementBackend.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> IsAuthenticated(string username, string password);
    }
}
