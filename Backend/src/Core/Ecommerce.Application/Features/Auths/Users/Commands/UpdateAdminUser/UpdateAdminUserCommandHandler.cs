using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommandHandler : IRequestHandler<UpdateAdminUserCommand, User>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public UpdateAdminUserCommandHandler(UserManager<User> userManager, 
        RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }
        public async Task<User> Handle(UpdateAdminUserCommand request, CancellationToken cancellationToken)
        {
            User updatedUser = await _userManager.FindByIdAsync(request.Id);
            if (updatedUser is null) throw new BadRequestException("El usuario no existe");

            updatedUser.Name = request.Name;
            updatedUser.LastName = request.LastName;
            updatedUser.Phone = request.Phone;

            var result = await _userManager.UpdateAsync(updatedUser);
            if (!result.Succeeded) throw new BadRequestException("No se pudo actualizat el usuario.");

            var role = await _roleManager.FindByNameAsync(request.rol);
            if (role is null) throw new BadRequestException("El usuario no existe");

            await _userManager.AddToRoleAsync(updatedUser, role.Name);
            return updatedUser;
        }
    }
}