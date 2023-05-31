using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infraestructure.MessageImplementation
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;

        }
        public async Task<bool> SenEmail(EmailMessage email, string token)
        {
            try
            {
                var client = new SendGridClient(_emailSettings.Key);
                var from = new EmailAddress(_emailSettings.Email);
                var subject = email.Subject;
                var to = new EmailAddress(email.To, email.To);
                var plainTextContent = email.Body;
                var htmlContent = $"{email.Body} {_emailSettings.BaseUrlCliente}/password/reset/{token}";

                var msg = MailHelper.CreateSingleEmail(
                    from, to, subject, plainTextContent, htmlContent
                );
                var response = await client.SendEmailAsync(msg);
                return response.IsSuccessStatusCode;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "No se pudo enviar");  
                return false;              
            }
        }
    }
}