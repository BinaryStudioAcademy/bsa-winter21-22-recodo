using MimeKit;
using System.Threading.Tasks;
namespace Thread_.NET.BLL.Services
{
    public class EmailService
    {
        public static async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Admin", "admin@mail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            //using (var client = new SmtpClient())
            //{
            //    await client.ConnectAsync("smtp.mail.io", 2525, false);
            //    await client.AuthenticateAsync("login", "password");
            //    await client.SendAsync(emailMessage);
            //    await client.DisconnectAsync(true);
            //}
        }
    }
}