using Application.DTO.User;
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

        public Task<bool> IsUserBlockedAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUsernameTakenAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task RegisterUserAsync(UserRequestDto dto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(int userId, UserRequestDto userDto)
        {
            throw new NotImplementedException();
        }

        
    }
}
