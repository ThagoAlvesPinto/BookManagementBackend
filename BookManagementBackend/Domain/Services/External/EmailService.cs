using BookManagementBackend.Domain.Interfaces.Services.External;

namespace BookManagementBackend.Domain.Services.External
{
    public class EmailService : IEmailService
    {
        public Task SendEmail(string to, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}
