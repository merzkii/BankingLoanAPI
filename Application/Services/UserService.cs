using Application.DTO;
using Core.Entities;
using Core.Interfaces;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task BlockUserAsync(int userId, bool isBlocked)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponseDto> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) throw new NotFoundException("User not found");

            return new UserResponseDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                MonthlyIncome = user.MonthlyIncome,
                IsBlocked = user.IsBlocked
            };
        }

        public async Task<UserResponseDto> RegisterUserAsync(UserRequestDto userDto)
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Username = userDto.Username,
                Age = userDto.Age,
                Email = userDto.Email,
                Password = HashPassword(userDto.Password)
            };

            await _userRepository.AddUserAsync(user);
            return new UserResponseDto { UserId = user.UserId, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email };
        }

        public Task UpdateUserAsync(int userId, UserRequestDto userDto)
        {
            throw new NotImplementedException();
        }

        private string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    }
}
