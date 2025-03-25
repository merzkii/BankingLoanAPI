using Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUserService
    {
        
        Task<UserResponseDto> GetUserByIdAsync(int userId);
        Task UpdateUserAsync(int userId, UserRequestDto userDto);
        Task BlockUserAsync(int userId, bool isBlocked);
    }
}
