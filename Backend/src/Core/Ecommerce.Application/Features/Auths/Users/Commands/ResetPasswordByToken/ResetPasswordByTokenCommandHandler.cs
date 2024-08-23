using System.Text;
using Ecommerce.Application.Exceptions;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.ResetPasswordByToken
{
    public class ResetPasswordByTokenCommandHandler : IRequestHandler<ResetPasswordByTokenCommand, string>
    {

        private readonly UserManager<User> _userManager;

        public ResetPasswordByTokenCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(ResetPasswordByTokenCommand request, CancellationToken cancellationToken)
        {
            if (!string.Equals(request.Password, request.ConfirmPassword)) 
                throw new BadRequestException("El password no es igual a la confirmación de password");

            User updateUser = await _userManager.FindByEmailAsync(request.Email!);
            if (updateUser is null)
                throw new BadRequestException("El emial no esta registrado como usuario.");

            byte[] token = Convert.FromBase64String(request.Token);
            string tokenResult = Encoding.UTF8.GetString(token);
            var resetResult = await _userManager.ResetPasswordAsync(updateUser, tokenResult, request.Password);
            
            if (!resetResult.Succeeded)
                throw new BadRequestException("No se pudo resetear el password.");


            return $"Se actualizó correctamente tu password ${request.Email}";
        }
    }
} 