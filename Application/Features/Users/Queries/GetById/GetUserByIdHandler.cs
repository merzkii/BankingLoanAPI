using Application.DTO.AdminUser;
using Application.DTO.Loan;
using Application.DTO.User;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponseDto>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetUserByIdHandler(IUserService userService, IMapper mapper,
            IAdminUserRepository adminUserRepository,
            IHttpContextAccessor httpContextAccessor,
            ICurrentUserService currentUserService)
        {
            _userService = userService;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<UserResponseDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var isOwnProfile = request.UserId == _currentUserService.UserId;
            var canViewAnyUser = _currentUserService.IsAdmin || _currentUserService.IsAccountant;

            if (!isOwnProfile && !canViewAnyUser)
            {
                throw new ForbiddenException("You do not have permission to view this user's profile.");
            }

            var user = await _userService.GetByIdAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            return _mapper.Map<UserResponseDto>(user);
        }
    }
}
