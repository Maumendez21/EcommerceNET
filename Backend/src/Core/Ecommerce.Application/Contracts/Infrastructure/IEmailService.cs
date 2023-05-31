using Ecommerce.Application.Models.Email;

namespace Ecommerce.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SenEmail(EmailMessage email, string token);
    }
}