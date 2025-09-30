using Application.DTO.AdminUser;
using Application.Exceptions;
using Application.Features.Users.Commands.Update;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.Admins.Commands.Update
{
    public class UpdateAdminUserHandler : IRequestHandler<UpdateAdminUserCommand, AdminUserResponseDTO>
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IMapper _mapper;

        public UpdateAdminUserHandler(IAdminUserRepository adminUserRepository, IMapper mapper)
        {
            _adminUserRepository = adminUserRepository;
            _mapper = mapper;
        }

        public async Task<AdminUserResponseDTO> Handle(UpdateAdminUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _adminUserRepository.GetByIdAsync(request.AdminUserId);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            user.Name = request.AdminUserData.firstName;
            user.Surname = request.AdminUserData.firstName;
            user.Username = request.AdminUserData.firstName;
            user.Email = request.AdminUserData.firstName;
            user.Role = request.AdminUserData.Role;

            await _adminUserRepository.UpdateAsync(user);

            return _mapper.Map<AdminUserResponseDTO>(user);
        }
    }
}
