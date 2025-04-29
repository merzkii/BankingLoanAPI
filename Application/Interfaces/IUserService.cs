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
        Task RegisterUserAsync(UserRequestDto dto);
        Task<bool> IsUserBlockedAsync(int userId);
        Task BlockUserAsync(int userId, bool isBlocked);
        Task<bool> IsUsernameTakenAsync(string username);
    }
}
