using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUserService
    {
        public Task<UserResponseDto> RegisterUserAsync(UserRequestDto userDto);
        Task<UserResponseDto> GetUserByIdAsync(int userId);
        Task UpdateUserAsync(int userId, UserRequestDto userDto);
        Task BlockUserAsync(int userId, bool isBlocked);
    }
}
