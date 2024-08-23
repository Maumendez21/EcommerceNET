using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;

        public ResetPasswordCommandHandler(UserManager<User> userManager,  IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            User updateUser = await _userManager.FindByNameAsync(_authService.GetSessionUser());

            if (updateUser is null)
                throw new BadRequestException("El usuario no existe");

            PasswordVerificationResult resultValidateOldPassword = _userManager.PasswordHasher
                                            .VerifyHashedPassword(updateUser, updateUser.PasswordHash, request.OldPassword);
            
            if (!(resultValidateOldPassword == PasswordVerificationResult.Success))
                throw new BadRequestException("El password ingresado es erroneo.");
            
            string hashedNewPassword = _userManager.PasswordHasher.HashPassword(updateUser, request.NewPassword);

            updateUser.PasswordHash = hashedNewPassword;
            var updateUserResult = await _userManager.UpdateAsync(updateUser);

            if (!updateUserResult.Succeeded)
                throw new BadRequestException("No se pudo resetear el password.");

            return Unit.Value;
        }
    }
}