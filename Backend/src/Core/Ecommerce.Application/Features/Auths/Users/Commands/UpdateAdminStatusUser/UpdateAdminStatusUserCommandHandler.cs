using Ecommerce.Application.Exceptions;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminStatusUser
{
    public class UpdateAdminStatusUserCommandHandler : IRequestHandler<UpdateAdminStatusUserCommand, User>
    {
        private readonly UserManager<User> _userManager;

        public UpdateAdminStatusUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> Handle(UpdateAdminStatusUserCommand request, CancellationToken cancellationToken)
        {
            User updatedUser = await _userManager.FindByIdAsync(request.Id);
            if (updatedUser is null) throw new BadRequestException("El usuario no existe");

            updatedUser.IsActive = !updatedUser.IsActive;
            var result = await _userManager.UpdateAsync(updatedUser);
            if (!result.Succeeded) throw new BadRequestException("No se pudo actualizar el estatus del usuario.");

            return updatedUser;
        }
    }
}