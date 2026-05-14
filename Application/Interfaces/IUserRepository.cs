using Core.Entities;
using Application.DTO.User;
namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task <List<User>> GetAllAsync();
        Task<(List<User> Items, int TotalCount)> GetPagedAsync(UserQueryParameters parameters);
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task <User?>GetByEmailAsync(string email);
    }
}
