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
            message.Subject = "إعادة ضبط كلمة المرور";
			message.Body = new TextPart("html")
			{
				Text = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif;'>
                        <div style='text-align: center;'>
                            <h1 style='color: #00008B;'>منصة بناء التعليمية</h1>
                            <p style='font-size: 18px;'>كودك المؤقت هو: <strong>{content}</strong></p>
                            <p style='font-size: 14px; color: #555;'>لا تشارك كودك مع أي شخص</p>
                        </div>
                    </body>
                    </html>"
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
