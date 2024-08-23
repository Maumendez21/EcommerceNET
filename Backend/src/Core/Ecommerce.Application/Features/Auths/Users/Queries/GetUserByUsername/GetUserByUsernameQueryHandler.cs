using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Queries.GetUserByUsername
{
    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, AuthResponse>
    {

        private readonly UserManager<User> _userManager;

        public GetUserByUsernameQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<AuthResponse> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {

            User user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null) throw new BadRequestException("El usuario no existe");

            return new AuthResponse{
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Phone = user.Phone,
                Email = user.Email,
                Username = user.UserName,
                Avatar = user.AvatarUrl,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }
    }
}