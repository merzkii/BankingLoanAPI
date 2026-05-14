using Core.Entities;

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
