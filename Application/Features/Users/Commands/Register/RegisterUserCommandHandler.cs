using Application.DTO.Auth;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using MediatR;
using Microsoft.AspNet.Identity;
using Application.Interfaces;
using Application.Exceptions;

namespace Application.Features.Users.Commands.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, LoginResponseDto>
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RegisterUserCommandHandler(
            IUserService userService,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userService.GetByUsernameAsync(request.Username);
            if (existingUser != null)
                throw new ValidationException("Username is already taken.");

            var existingEmail = await _userService.GetByEmailAsync(request.Email); 
            if (existingEmail != null)
                throw new ValidationException("Email is already in use.");

           
            var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Email = request.Email,
                Age = request.Age,
                MonthlyIncome = request.MonthlyIncome,
                IsBlocked = false,
                UserType = request.UserType,
                Password = hashedPassword
            };

            await _userService.RegisterAsync(user);

           
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }
    }

}
