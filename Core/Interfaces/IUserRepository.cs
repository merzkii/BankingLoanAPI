using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Register(UserRequestDto name);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
