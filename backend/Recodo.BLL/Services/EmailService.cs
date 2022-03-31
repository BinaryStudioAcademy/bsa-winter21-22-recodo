using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Recodo.BLL.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            string apiKey = _configuration["SendGridKey"];
            string fromEmail = _configuration["SendGridFromEmail"];
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress(fromEmail, "Admin Recodo");
            var to = new EmailAddress(toEmail, toEmail);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
            await client.SendEmailAsync(msg);
        }
    }
}