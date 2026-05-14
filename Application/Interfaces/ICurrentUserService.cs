using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        string? Role { get; }
        bool IsAuthenticated { get; }
        bool IsAdmin { get; }
        bool IsAccountant { get; }
        bool IsUser { get; }
    }
}
