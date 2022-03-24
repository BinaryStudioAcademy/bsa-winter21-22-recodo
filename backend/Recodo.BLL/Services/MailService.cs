using MimeKit;
using MimeKit.Text;

namespace Recodo.BLL.Services
{
    public sealed class MailService
    {
        public void SendEmail(string body, string email, string name = "")
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Recodo", "recodo.app.22@gmail.com"));
            message.To.Add(new MailboxAddress(name, email));
            message.Subject = $"{name} shared a video with you";
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = $"{name} sent tou a video. You can watch this video by the link: {body}"
            };

            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("recodo.app.22@gmail.com", "recodo_22");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
