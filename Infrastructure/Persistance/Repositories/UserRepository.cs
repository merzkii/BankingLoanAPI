using Application.DTO.User;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserRepository _userRepository;

        public UserRepository(IUserRepository userRepository)
        {

            _userRepository = userRepository;
        }

        public Task AddAsync(User user)
        {
            throw new NotImplementedException();
        }

       
        public Task DeleteAsync(int id)
        {
            return _userRepository.DeleteAsync(id);
        }

        public Task<User?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(User user)
        {
          return _userRepository.UpdateAsync(user);
        }

        private string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    }
}
