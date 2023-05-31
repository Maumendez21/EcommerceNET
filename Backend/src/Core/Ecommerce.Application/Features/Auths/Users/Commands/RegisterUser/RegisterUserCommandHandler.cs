using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public RegisterUserCommandHandler(
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            bool existUserByEmail = await _userManager.FindByEmailAsync(request.Email) is null ? false : true;
            if (existUserByEmail) throw new BadRequestException("This email is already use");
            
            bool existUserByUsername = await _userManager.FindByNameAsync(request.UserName) is null ? false : true;
            if (existUserByUsername) throw new BadRequestException("This username is already use");

            User user = new User
            {
                Name = request.Name,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email,
                UserName = request.UserName,
                AvatarUrl = request.AvatarUrl,

            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded) throw new Exception("Failed to register user");

            await _userManager.AddToRoleAsync(user, AppRole.GenericUser);
            IList<string> roles = await _userManager.GetRolesAsync(user);
            return new AuthResponse{
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Phone = user.Phone,
                Username = user.UserName,
                Email = user.Email,
                Token = _authService.CreateToken(user, roles),
                Avatar = user.AvatarUrl,
                Roles = roles
            };

        }
    }
}