using Application.DTO.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Update
{
    public class UpdateUserCommand : IRequest<UserResponseDto>
    {
        public int UserId { get; set; }
        public UserRequestDto UserData { get; set; }

        public UpdateUserCommand(int userId, UserRequestDto userData)
        {
            UserId = userId;
            UserData = userData;
        }
    }
}
