using Application.DTO.AdminUser;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admins.Queries.GetAll
{
    public class GetAllAdminsQueryHandler: IRequestHandler<GetAllAdminsQuery, List<AdminUserResponseDTO>>
    {
        private readonly IAdminUserRepository _adminUserRepository;
        public GetAllAdminsQueryHandler(IAdminUserRepository adminUserRepository)
        {
            _adminUserRepository = adminUserRepository;
        }
        public async Task<List<AdminUserResponseDTO>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
        {
            var adminUsers = await _adminUserRepository.GetAllAsync();
            return adminUsers.Select(adminUser => new AdminUserResponseDTO
            {
                Id = adminUser.Id,
                Name = adminUser.Name,
                Surname = adminUser.Surname,
                Username = adminUser.Username,
                Email = adminUser.Email,
                Role = adminUser.Role
            }).ToList();
        }
    }
}
