using Application.Features.Users.Commands.Block;
using FluentValidation;

namespace Application.Validations.User
{
    public class BlockUserCommandValidator : AbstractValidator<BlockUserCommand>
    {
        public BlockUserCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}
