using System.Text;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Models.Email;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.SendPassword
{
    public class SendPasswordCommandHandler : IRequestHandler<SendPasswordCommand, string>
    {

        private readonly IEmailService _serviceEmail;
        private readonly UserManager<User> _userManager;

        public SendPasswordCommandHandler(IEmailService serviceEmail, UserManager<User> userManager)
        {
            _serviceEmail = serviceEmail;
            _userManager = userManager;
        }

        public async Task<string> Handle(SendPasswordCommand request, CancellationToken cancellationToken)
        {
            var user  = await _userManager.FindByEmailAsync(request.Email!);

            if (user is null)
            {
                throw new BadRequestException("El usuario no existe.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var plainTextBytes = Encoding.UTF8.GetBytes(token);
            token = Convert.ToBase64String(plainTextBytes);
            var emailMessage = new EmailMessage
            {
                To = request.Email,
                Body = "Resetea el passwor, da click aquí: ",
                Subject = "Cambiar el password"
            };
            var result = await _serviceEmail.SenEmail(emailMessage, token);
            if (!result)
            {
                throw new Exception("No se pudo enviar el email.");
            }

            return $"Se envio el email a la cuenta {request.Email}";
        }
    }
}