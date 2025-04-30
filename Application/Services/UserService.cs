using Application.DTO.User;
using Core.Entities;
using Core.Interfaces;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Task<User?> GetByUsernameAsync(string username) => _userRepository.GetByUsernameAsync(username);

        public Task RegisterAsync(User user) => _userRepository.AddAsync(user);
        public Task DeleteAsync(User user) => _userRepository.DeleteAsync(user.UserId);
        public Task UpdateAsync(User user) => _userRepository.UpdateAsync(user);
    }

}

