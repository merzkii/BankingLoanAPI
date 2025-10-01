using Application.DTO.AdminUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admins.Queries.GetById
{
    public class GetAdminByIdQuery: IRequest<AdminUserResponseDTO>
    {
        public int AdminId { get; set; }
        public GetAdminByIdQuery(int adminId)
        {
            AdminId = adminId;
        }
    }
}
