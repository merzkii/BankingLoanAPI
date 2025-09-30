using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admins.Commands.Delete
{
    public class DeleteAdminUserHandler : IRequestHandler<DeleteAdminUserCommand, int>
    {
        private readonly IAdminUserRepository _adminUserRepository;
        public DeleteAdminUserHandler(IAdminUserRepository adminUserRepository)
        {
            _adminUserRepository = adminUserRepository;
        }
        public async Task<int> Handle(DeleteAdminUserCommand request, CancellationToken cancellationToken)
        {
            var adminUser = await _adminUserRepository.GetByIdAsync(request.AdminUserId);
            if (adminUser == null)
            {
                throw new KeyNotFoundException("Admin user not found.");
            }
            await _adminUserRepository.DeleteAsync(adminUser.Id);
            return adminUser.Id;
        }
    }
}
