using Application.DTO.Auth;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IPasswordHasher<AdminUsers> _adminPasswordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUserCommandHandler(
            IUserRepository userRepository,
            IAdminUserRepository adminUserRepository,
            IPasswordHasher<User> passwordHasher,
            IPasswordHasher<AdminUsers> adminPasswordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _adminUserRepository = adminUserRepository;
            _passwordHasher = passwordHasher;
            _adminPasswordHasher = adminPasswordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null ||
                _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password) == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1)
            };

            var adminUser = await _adminUserRepository.GetByUsernameAsync(request.Username);
            if (adminUser == null ||
                _adminPasswordHasher.VerifyHashedPassword(adminUser, adminUser.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }
            var adminToken = _jwtTokenGenerator.GenerateToken(adminUser);
            return new LoginResponseDto
            {
                Token = adminToken,
                Expiration = DateTime.UtcNow.AddHours(1)
            };

            throw new UnauthorizedAccessException("Invalid username or password."); 
        }
    }
    
}
