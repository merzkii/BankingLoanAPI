using Application.DTO.AdminUser;
using Application.Interfaces;
using Core.Entities.Admins;
using MapsterMapper;
using MediatR;

namespace Application.Features.Admins.Commands.Add
{
    public class AddAdminUserHandler: IRequestHandler<AddAdminUserCommand, AdminUserResponseDto>
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IMapper _mapper;

        public AddAdminUserHandler(IAdminUserRepository adminUserRepository, IMapper mapper)
        {
            _adminUserRepository = adminUserRepository;
            _mapper = mapper;
        }

        public async Task<AdminUserResponseDto> Handle(AddAdminUserCommand request, CancellationToken cancellationToken)
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
            return _mapper.Map<AdminUserResponseDto>(newAdminUser);
        }
    }
}
