using Application.DTO.User;
using MediatR;

namespace Application.Features.Users.Queries.GetById
{
    public class GetUserByIdQuery : IRequest<UserResponseDto>
    {
        public int UserId { get; set; }
        public GetUserByIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
