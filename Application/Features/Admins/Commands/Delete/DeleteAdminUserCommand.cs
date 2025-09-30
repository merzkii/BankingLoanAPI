using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admins.Commands.Delete
{
    public class DeleteAdminUserCommand: IRequest<int>
    {
        public int AdminUserId { get; set; }
       
    }   
   
}
