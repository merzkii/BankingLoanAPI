using Application.Exceptions;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Block
{
    public class BlockUserCommandHandler : IRequestHandler<BlockUserCommand, Unit>
    {
        private readonly IUserService _userService;
        public BlockUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Unit> Handle(BlockUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }
            user.IsBlocked = true;
            await _userService.UpdateAsync(user);
            return Unit.Value;
        }
    }
}
