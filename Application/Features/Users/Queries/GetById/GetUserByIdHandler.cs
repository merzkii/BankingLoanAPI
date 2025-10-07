using Application.DTO.Loan;
using Application.DTO.User;
using Application.Exceptions;
using AutoMapper;
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
    public class GetUserByIdHandler: IRequestHandler<GetUserByIdQuery, UserResponseDto>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetUserByIdHandler(IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserResponseDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (currentUserId != request.UserId)
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
