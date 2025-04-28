using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Block
{
    class BlockUserCommand
    {
        public int UserId { get; set; }
        public BlockUserCommand(int userId)
        {
            UserId = userId;
        }
    }
}
