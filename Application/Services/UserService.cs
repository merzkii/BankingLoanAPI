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
        private readonly IUserService _userService;

        public UserService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task BlockUserAsync(int userId, bool isBlocked)
        {
            var blockUser=_userService.BlockUserAsync(userId, isBlocked);
           
        }

        public async Task<UserResponseDto> GetUserByIdAsync(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
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

       
        public Task UpdateUserAsync(int userId, UserRequestDto userDto)
        {
            throw new NotImplementedException();
        }

        
    }
}
