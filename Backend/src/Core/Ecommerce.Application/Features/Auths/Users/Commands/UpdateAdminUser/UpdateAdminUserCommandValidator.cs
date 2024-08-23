using FluentValidation;

namespace Ecommerce.Application.Features.Auths.Users.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommandValidator : AbstractValidator<UpdateAdminUserCommand>
    {
        public UpdateAdminUserCommandValidator()
        {
            RuleFor(p => p.Name)
            .NotEmpty().WithMessage("El Nombre del usuario no puede ser nulo.");
            RuleFor(p => p.LastName)
            .NotEmpty().WithMessage("El Apellido del usuario no puede ser nulo.");
        }
        
    }
}