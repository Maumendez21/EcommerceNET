using Ecommerce.Application.Features.Auths.Users.Vms;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Auths.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<AuthResponse>
    {
        public string? Name     { get; set; }
        public string? LastName { get; set; }
        public string? Email    { get; set; }
        public string? Phone    { get; set; }
        public IFormFile? Avatar { get; set; }
        public string? AvatarUrl { get; set; }
        public string? AvatarId { get; set; }
    }
}