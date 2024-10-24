using BookManagementBackend.Classes;
using BookManagementBackend.Domain.Interfaces.Services.External;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BookManagementBackend.Domain.Services.External
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _settings;

        public EmailService(IOptions<AppSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendEmail(string to, string subject, string body)
        {
            string apiKey = _settings.SendGridKey;

            SendGridClient client = new(apiKey);

            SendGridMessage msg = new()
            {
                From = new EmailAddress("projetointegrador2grupo18@outlook.com", "DEVOLUWEB"),
                
                HtmlContent = body,
                Subject = subject
            };

            msg.AddTo(new EmailAddress(to));

            Response response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Body.ReadAsStringAsync());
        }
    }
}
