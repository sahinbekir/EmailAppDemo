using EmailAppDemo.Models;

namespace EmailAppDemo.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto model);
    }
}
