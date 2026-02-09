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
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetUserByIdHandler(IUserService userService, IMapper mapper, IAdminUserRepository adminUserRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _mapper = mapper;
            _adminUserRepository = adminUserRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserResponseDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null || httpContext.User == null)
                throw new UnauthorizedAccessException("No user context found.");

            var nameIdentifierClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (nameIdentifierClaim == null)
                throw new UnauthorizedAccessException("User identifier claim not found.");

            var currentUserId = int.Parse(nameIdentifierClaim.Value);

            
            var adminUser = await _adminUserRepository.GetByIdAsync(currentUserId);
            bool isAdmin = adminUser != null;

            if (currentUserId != request.UserId && !isAdmin)
            {
                throw new UnauthorizedAccessException("You are not allowed to view other users' information.");
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
