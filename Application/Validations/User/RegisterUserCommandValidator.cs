using Application.Features.Users.Commands.Register;
using FluentValidation;

namespace Application.Validations.User
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MinimumLength(4);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        }
    }
}
