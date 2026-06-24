using Application.DTO.AdminUser;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Admins.Queries.GetAll
{
    public class GetAllAdminsQueryHandler: IRequestHandler<GetAllAdminsQuery, List<AdminUserResponseDto>>
    {
        private readonly IAdminUserRepository _adminUserRepository;
        public GetAllAdminsQueryHandler(IAdminUserRepository adminUserRepository)
        {
            _adminUserRepository = adminUserRepository;
        }
        public async Task<List<AdminUserResponseDto>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
        {
            var adminUsers = await _adminUserRepository.GetAllAsync();
            return adminUsers.Select(adminUser => new AdminUserResponseDto
            {
                Id = adminUser.Id,
                Name = adminUser.Name ?? string.Empty,
                Surname = adminUser.Surname ?? string.Empty,
                Username = adminUser.Username ?? string.Empty,
                Email = adminUser.Email ?? string.Empty,
                Role = adminUser.Role
            }).ToList();
        }
    }
}
