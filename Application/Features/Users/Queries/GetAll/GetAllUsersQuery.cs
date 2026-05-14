using Application.DTO.Common;
using Application.DTO.User;
using MediatR;

namespace Application.Features.Users.Queries.GetAll
{
    public record GetAllUsersQuery : UserQueryParameters, IRequest<PagedResult<UserResponseDto>>;
}
