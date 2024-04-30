using MimeKit;
using Benaa.Core.Interfaces.IServices;
namespace Benaa.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly string smtpHost = "smtp.gmail.com";
        private readonly int smtpPort = 587;
        private readonly string smtpUsername = "yazedir45@gmail.com";
        private readonly string smtpPassword = "japuodzmwdgjewon";

        public void SendEmailAsync(string email, string content)
        {
            var message = new MimeMessage();
            MailboxAddress emailSedner = new MailboxAddress("منصة بناء التعليمية", smtpUsername);
            MailboxAddress emailReciever = new MailboxAddress("", email);
            message.From.Add(emailSedner);
            message.To.Add(emailReciever);
            message.Subject = "Email Confirmation";
            message.Body = new TextPart("plain")
            {
                Text = $"منصة بناء التعليمية\n" +
                $" كودك المؤقت هو: {content}\n" +
                $"لا تشارك كودك مع اي شخص"
            };

            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(smtpUsername, smtpPassword);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
