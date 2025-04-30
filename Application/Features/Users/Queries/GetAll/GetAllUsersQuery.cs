using Application.DTO.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetAll
{
    public class GetAllUsersQuery : IRequest<List<UserResponseDto>>
    {
    }
}
