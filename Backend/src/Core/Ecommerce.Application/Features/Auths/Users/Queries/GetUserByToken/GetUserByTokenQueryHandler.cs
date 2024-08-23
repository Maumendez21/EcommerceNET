using AutoMapper;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Features.Auths.Users.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Queries.GetUserByToken
{
    public class GetUserByTokenQueryHandler : IRequestHandler<GetUserByTokenQuery, AuthResponse>
    {

        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByTokenQueryHandler(UserManager<User> userManager,  
        IAuthService authService, 
        IUnitOfWork unitOfWork,
        IMapper mapper)
        {
            _userManager = userManager;
            _authService = authService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AuthResponse> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_authService.GetSessionUser());
            if (user is null) throw new BadRequestException("El usuario no existe");
            if (!user.IsActive) throw new Exception("El usuario esta bloqueado, contacta al administrador.");

            Address sendAddress = await _unitOfWork.Repository<Address>().GetEntityAsync(
                x => x.AddressUserName == user.UserName
            ) ;

            IList<string> roles = await _userManager.GetRolesAsync(user);
            return new AuthResponse{
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Phone = user.Phone,
                Email = user.Email,
                Username = user.UserName,
                Avatar = user.AvatarUrl,
                Roles = roles,
                ShippingAddress = _mapper.Map<AddressVm>(sendAddress),
                Token = _authService.CreateToken(user, roles)
            };

        }
    }
}