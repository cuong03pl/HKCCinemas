using HKCCinemas.Models;

namespace HKCCinemas.Interfaces
{
    public interface ISendMailService
    {
        Task<bool> SendMail(MailContent mailContent);

        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
