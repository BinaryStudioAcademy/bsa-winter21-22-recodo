using MimeKit;
using MimeKit.Text;

namespace Recodo.BLL.Services
{
    public sealed class MailService
    {
        public void SendEmail(string body, string email, string name = "")
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Recodo", "m.shmatko2002@gmail.com"));
            message.To.Add(new MailboxAddress(name, email));
            message.Subject = "Message from recodo";
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };

            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("m.shmatko2002@gmail.com", "380985883147");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
