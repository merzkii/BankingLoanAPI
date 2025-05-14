using Application.DTO.User;
using FluentValidation;

namespace Application.Validations.User
{
    public class UserRequestValidator: AbstractValidator<UserRequestDto>
    {

        public UserRequestValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(u => u.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required.");
            RuleFor(u => u.Password).MinimumLength(6).WithMessage("Password should be at least 6 characters.");
        }
    }
}
