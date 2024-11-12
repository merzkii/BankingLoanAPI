using Application.DTO;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserRepository _userRepository;

        public UserRepository(IUserRepository userRepository)
        {

            _userRepository = userRepository;
        }
        

        public Task DeleteUserAsync(int id)
        {
            return _userRepository.DeleteUserAsync(id);
        }

        public Task<User> Register(UserRequestDto name)
        {
            var register= _userRepository.Register(name);
            var hash=HashPassword(name.Password);
            return register;    
        }

        public Task UpdateUserAsync(User user)
        {
          return _userRepository.UpdateUserAsync(user);
        }

        private string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    }
}
