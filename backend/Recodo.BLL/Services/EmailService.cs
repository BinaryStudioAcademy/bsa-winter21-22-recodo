using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Recodo.BLL.Services
{
    public class EmailService
    {
        public static async Task SendEmailAsync(string toEmail, string subject, string message, IConfiguration configuration, string workspaceName = "")
        {
            string apiKey = configuration["SendGridKey"];
            string fromEmail = configuration["SendGridFromEmail"];
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress(fromEmail, "Admin Recodo");
            var to = new EmailAddress(toEmail, toEmail);

            if(workspaceName != "")
            {
                message = $"{workspaceName} shared a video with you: {message}";
            }

            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
            await client.SendEmailAsync(msg);
        }
    }
}