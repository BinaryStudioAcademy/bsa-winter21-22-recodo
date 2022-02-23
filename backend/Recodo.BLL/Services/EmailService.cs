using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Thread_.NET.BLL.Services
{
    public class EmailService
    {
        public static async Task SendEmailAsync(string email, string subject, string message, IConfiguration configuration)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("admin@recodo.com", "admin@recodo.com"));
            emailMessage.To.Add(new MailboxAddress(email, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(configuration["SmtpServer"], Convert.ToInt32(configuration["SmtpPort"]), false);
                await client.AuthenticateAsync(configuration["SmtpLogin"], configuration["SmtpPassword"]);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }

            Console.WriteLine(message);
        }
    }
}