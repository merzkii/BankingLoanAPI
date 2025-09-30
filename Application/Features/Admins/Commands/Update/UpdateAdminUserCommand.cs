using Application.DTO.AdminUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admins.Commands.Update
{
    public class UpdateAdminUserCommand: IRequest<AdminUserResponseDTO>
    {
        public int AdminUserId { get; set; }
        public AdminUserRequestDTO AdminUserData { get; set; }
        public UpdateAdminUserCommand(int adminUserId, AdminUserRequestDTO adminUserData)
        {
            AdminUserId = adminUserId;
            AdminUserData = adminUserData;
        }
    }
}
