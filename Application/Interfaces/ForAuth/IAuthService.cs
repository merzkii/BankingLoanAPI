using Application.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ForAuth
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(string username, string password);
        Task<LoginResponseDto> RefreshTokenAsync(string rawRefreshToken);
        Task RevokeAsync(string refreshToken);
    }
}
