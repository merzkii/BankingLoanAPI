using Application.DTO.User;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id);
        Task<List<User>> GetAllAsync();
        Task<User?> GetByUsernameAsync(string username);
        Task RegisterAsync(User user);
        Task DeleteAsync(int userId);
        Task UpdateAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}
