using BookManagementBackend.Classes;
using BookManagementBackend.Domain.Interfaces.Services.External;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace BookManagementBackend.Domain.Services.External
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _settings;

        public EmailService(IOptions<AppSettings> options) 
        {
            _settings = options.Value;
        }

        public Task SendEmail(string to, string subject, string body)
        {
            SmtpClient client = new(_settings.SMTPServer, _settings.SMTPPort)
            {
                Credentials = new System.Net.NetworkCredential(_settings.Email, _settings.Password),
                EnableSsl = _settings.EnableSSL               
            };

            MailMessage mail = new(_settings.Email, to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
                Sender = new(_settings.Email, _settings.EmailDisplayName)
            };

            return client.SendMailAsync(mail);
        }
    }
}
