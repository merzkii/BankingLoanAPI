using Application.DTO.Auth;
using Application.Exceptions;
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

namespace Application.Features.Admins.Commands.Create
{
    public class CreateAdminHandler : IRequestHandler<CreateAdminCommand, LoginResponseDto>
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IPasswordHasher<AdminUsers> _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public CreateAdminHandler(IAdminUserRepository adminUserRepository, IPasswordHasher<AdminUsers> passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _adminUserRepository = adminUserRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponseDto> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _adminUserRepository.GetByUsernameAsync(request.Username);
            if (existingUser != null)
                throw new ValidationException("Username is already taken.");
            //var existingEmail = await _adminUserRepository.GetByEmailAsync(request.Email);
            //if (existingEmail != null)
            //    throw new ValidationException("Email is already in use.");
            var adminUser = new AdminUsers
            {
                Name = request.FirstName,
                Surname = request.LastName,
                Username = request.Username,
                Email = request.Email,
                Role = request.Role,
                PasswordHash = _passwordHasher.HashPassword(null, request.Password)
            };

            _adminUserRepository.AddAsync(adminUser);
            var token = _jwtTokenGenerator.GenerateToken(adminUser);
            return new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }
    }
}
