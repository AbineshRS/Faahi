using MailKit.Security;
using MimeKit;
namespace Faahi.Service.Email
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Faahi", _configuration["MailSettings:SenderEmail"]));
            emailMessage.To.Add(new MailboxAddress(recipientEmail, recipientEmail));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.CheckCertificateRevocation = false;
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await smtp.ConnectAsync(_configuration["MailSettings:SmtpServer"], 587, SecureSocketOptions.StartTls);

                await smtp.AuthenticateAsync(_configuration["MailSettings:SenderEmail"], _configuration["MailSettings:SenderPassword"]);

                await smtp.SendAsync(emailMessage);
                await smtp.DisconnectAsync(true);
            }
        }

    }
}
