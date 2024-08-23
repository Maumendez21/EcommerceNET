using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommand : IRequest<User>
    {
        public string Id { get; set; }  = string.Empty;
        public string Name { get; set; }  = string.Empty;
        public string LastName { get; set; }  = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string rol { get; set; }  = string.Empty;
    }
}