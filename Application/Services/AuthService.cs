using Application.DTO.Auth;
using Application.Interfaces.ForAuth;
using Core.Entities.Auth;
using Core.Entities.Users;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginResponseDto> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username)
               ?? throw new UnauthorizedAccessException("Invalid Credentials");

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password!, password);
          
            if(result == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid Credentials");
            }
                
            var accessToken = _jwtTokenGenerator.GenerateToken(user);
            var rawRefresh = _jwtTokenGenerator.GenerateRefreshToken();
            var refreshHash = _jwtTokenGenerator.HashToken(rawRefresh);

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                TokenHash = refreshHash,
                UserId = user.UserId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            });

            await _refreshTokenRepository.SaveChangesAsync();

            return new LoginResponseDto 
            { 
                Token = accessToken, 
                Expiration = DateTime.UtcNow.AddHours(1), 
                RefreshToken = rawRefresh
            };
        }
    }
}
