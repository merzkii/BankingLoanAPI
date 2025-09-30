using Application.DTO.AdminUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admins.Commands.Add
{
    public class AddAdminUserCommand:IRequest<AdminUserResponseDTO>
    {
        public AdminUserRequestDTO AdminUserData { get; set; }
        
    }
}
