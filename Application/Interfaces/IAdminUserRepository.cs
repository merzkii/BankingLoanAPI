using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public  interface IAdminUserRepository
    {
        Task<AdminUsers?> GetByIdAsync(int id);
        Task<List<AdminUsers>> GetAllAsync();
        Task<AdminUsers?> GetByUsernameAsync(string username);
        Task AddAsync(AdminUsers adminUser);
        Task UpdateAsync(AdminUsers adminUser);
        Task DeleteAsync(int id);
    }
}
