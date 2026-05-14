using Application.Interfaces;
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
        private readonly ICurrentUserService _currentUserService;

        public DeleteUserCommandHandler(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var isSelf = request.UserId == _currentUserService.UserId;

            if(!isSelf && !_currentUserService.IsAdmin)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this user.");
            }

            var user = await _userService.GetByIdAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }
              
            await _userService.DeleteAsync(user.UserId);

            return Unit.Value;
        }
    }

}
