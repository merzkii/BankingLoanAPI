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
    public class GetAllAdminsQuery: IRequest<List<AdminUserResponseDTO>>
    {
       
    }
}
