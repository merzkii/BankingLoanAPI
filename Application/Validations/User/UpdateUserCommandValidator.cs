using Application.Features.Users.Commands.Update;
using FluentValidation;

namespace Application.Validations.User
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.UserData.FirstName).NotEmpty();
            RuleFor(x => x.UserData.LastName).NotEmpty();
            RuleFor(x => x.UserData.Email).EmailAddress();
        }
    }
}
