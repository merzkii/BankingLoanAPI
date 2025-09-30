using Application.DTO.AdminUser;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admins.Commands.Add
{
    public class AddAdminUserHandler: IRequestHandler<AddAdminUserCommand, AdminUserResponseDTO>
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IMapper _mapper;

        public AddAdminUserHandler(IAdminUserRepository adminUserRepository)
        {
            _adminUserRepository = adminUserRepository;
        }

        public async Task<AdminUserResponseDTO> Handle(AddAdminUserCommand request, CancellationToken cancellationToken)
        {
            var newAdminUser = new AdminUsers
            {
                Name = request.AdminUserData.firstName,
                Surname = request.AdminUserData.lastName,
                Username = request.AdminUserData.Username,
                Email = request.AdminUserData.Email,
                PasswordHash = request.AdminUserData.Password,
                Role = request.AdminUserData.Role

            };
             await _adminUserRepository.AddAsync(newAdminUser);
            return _mapper.Map<AdminUserResponseDTO>(newAdminUser);
        }
    }
}
