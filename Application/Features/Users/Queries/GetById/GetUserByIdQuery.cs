using Application.DTO.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetById
{
    class GetUserByIdQuery: IRequest<UserResponseDto>
    {
        public int UserId { get; set; }
        public GetUserByIdQuery(int userId)
        {
            UserId = userId;
        }
    }
   
}
