using Application.DTO;
using Core.Entities;
using Core.Interfaces;

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

        private string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    }
}
