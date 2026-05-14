using Application.DTO.User;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User?> GetByIdAsync(int id) => _userRepository.GetByIdAsync(id);
        public Task<List<User>> GetAllAsync() => _userRepository.GetAllAsync();
        public Task<(List<User> Items, int TotalCount)> GetPagedAsync(UserQueryParameters parameters) => _userRepository.GetPagedAsync(parameters);
        public Task<User?> GetByUsernameAsync(string username) => _userRepository.GetByUsernameAsync(username);
        public Task<User?> GetByEmailAsync(string email) => _userRepository.GetByEmailAsync(email);
        public Task RegisterAsync(User user) => _userRepository.AddAsync(user);
        public Task DeleteAsync(int userId) => _userRepository.DeleteAsync(userId);
        public Task UpdateAsync(User user) => _userRepository.UpdateAsync(user);
    }
}

