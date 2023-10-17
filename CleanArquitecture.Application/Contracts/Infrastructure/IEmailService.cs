using CleanArquitecture.Application.Models.Email;

namespace CleanArquitecture.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Email email);
    }
}
