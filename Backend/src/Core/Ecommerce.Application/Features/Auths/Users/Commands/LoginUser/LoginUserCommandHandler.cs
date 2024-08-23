using AutoMapper;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LoginUserCommandHandler(
            IAuthService authService,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _authService = authService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) throw new NotFoundException(nameof(User), request.Email!);
            if (!user.IsActive) throw new Exception("User is blocked");

            SignInResult resultSingIn = await _signInManager.CheckPasswordSignInAsync(user, request.Password!, false);
            if (!resultSingIn.Succeeded) throw new Exception("Incorrect credentials");

            Address shippingAddress = await _unitOfWork.Repository<Address>().GetEntityAsync(
                x => x.AddressUserName == user.UserName
            );

            IList<string> roles = await _userManager.GetRolesAsync(user);

            return new AuthResponse
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Phone = user.Phone,
                Username = user.UserName,
                Email = user.Email,
                Token = _authService.CreateToken(user, roles),
                Avatar = user.AvatarUrl,
                ShippingAddress = _mapper.Map<AddressVm>(shippingAddress),
                Roles = roles
            };
        }
    }
}