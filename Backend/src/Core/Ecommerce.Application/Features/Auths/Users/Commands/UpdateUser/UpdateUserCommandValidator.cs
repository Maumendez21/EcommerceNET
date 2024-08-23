using FluentValidation;

namespace Ecommerce.Application.Features.Auths.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(p => p.Name)
            .NotEmpty().WithMessage("El Nombre del usuario no puede ser nulo.");
            RuleFor(p => p.LastName)
            .NotEmpty().WithMessage("El Apellido del usuario no puede ser nulo.");
        }
    }
}