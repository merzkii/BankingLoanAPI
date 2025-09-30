using Application.DTO.User;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetAll
{
    class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponseDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponseDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(user => new UserResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                UserType = user.UserType,
                IsBlocked = user.IsBlocked
            }).ToList();

            //return users.Select(user => new UserResponseDto
            //{
            //    UserId = user.UserId,
            //    Username = user.Username,
            //    Email = user.Email,
            //    UserRole = user.UserRole,
            //    IsBlocked = user.IsBlocked
            //}).ToList();
        }
    }
}

