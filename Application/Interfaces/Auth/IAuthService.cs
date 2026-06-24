using Application.DTO.Auth;

namespace Application.Interfaces.ForAuth
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(string username, string password);
        Task<LoginResponseDto> RefreshTokenAsync(string rawRefreshToken);
        Task RevokeAsync(string refreshToken);
    }
}
