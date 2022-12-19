using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace EmailAppDemo.Services.EmailService
{
    public class EmailManager : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailManager(IConfiguration config)
        {
            _config = config;
        }
        public void SendEmail(EmailDto model)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(model.To));
            email.Subject = model.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = model.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
