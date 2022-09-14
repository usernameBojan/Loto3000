using Loto3000.Application.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace Loto3000.Infrastructure
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SendEmail(string subject, string body, string recieverEmailAddress)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(configuration["Email:Sender"]));
            email.To.Add(MailboxAddress.Parse(recieverEmailAddress));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body};
            using var smtp = new SmtpClient();
            smtp.Connect(configuration["Email:Host"], 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(configuration["Email:Sender"], configuration["Email:Pw"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}