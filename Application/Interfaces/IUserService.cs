using Application.DTO.User;
using Core.Entities;

namespace Application
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id);
        Task<List<User>> GetAllAsync();
        Task<(List<User> Items, int TotalCount)> GetPagedAsync(UserQueryParameters parameters);
        Task<User?> GetByUsernameAsync(string username);
        Task RegisterAsync(User user);
        Task DeleteAsync(int userId);
        Task UpdateAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}
