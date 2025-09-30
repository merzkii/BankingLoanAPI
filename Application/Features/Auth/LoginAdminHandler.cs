using Application.DTO.Auth;
using Application.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth
{
    public class LoginAdminHandler: IRequestHandler<LoginAdminCommand, LoginResponseDto>
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IPasswordHasher<AdminUsers> _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public async Task<LoginResponseDto> Handle(LoginAdminCommand request, CancellationToken cancellationToken)
        {
            var adminUser = await _adminUserRepository.GetByUsernameAsync(request.Username);
            if (adminUser == null ||
               _passwordHasher.VerifyHashedPassword(adminUser, adminUser.PasswordHash, request.Password) == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            var token = _jwtTokenGenerator.GenerateToken(adminUser); 
            return new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }
    }
}
