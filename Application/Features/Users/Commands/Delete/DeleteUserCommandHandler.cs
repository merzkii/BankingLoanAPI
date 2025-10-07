using Core.Interfaces;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserService _userService;
        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.UserId);
            if (user == null)
                throw new NotFoundException("User not found.");

            await _userService.DeleteAsync(user.UserId);
            return Unit.Value;
        }
    }
   
}
