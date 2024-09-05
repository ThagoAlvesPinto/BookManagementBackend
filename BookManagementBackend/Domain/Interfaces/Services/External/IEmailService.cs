namespace BookManagementBackend.Domain.Interfaces.Services.External
{
    public interface IEmailService
    {
        Task SendEmail(string to, string subject, string body);
    }
}
