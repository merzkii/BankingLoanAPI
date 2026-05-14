using Application.DTO.User;
using MediatR;

namespace Application.Features.Users.Commands.Update
{
    public record UpdateUserCommand : IRequest<UserResponseDto>
    {
        public int UserId { get; init; }
        public UserRequestDto UserData { get; init; } = new();

        public UpdateUserCommand() { }

        public UpdateUserCommand(int userId, UserRequestDto userData)
        {
            UserId = userId;
            UserData = userData;
        }
    }
}
