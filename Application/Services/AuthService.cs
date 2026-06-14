using Application;
using Application.DTO.Auth;
using Application.Interfaces;
using Application.Interfaces.ForAuth;
using Core.Entities.Admins;
using Core.Entities.Auth;
using Core.Entities.Users;
using Microsoft.AspNetCore.Identity;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IAdminUserRepository _adminUserRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepo;
    private readonly IJwtTokenGenerator _jwtGenerator;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IPasswordHasher<AdminUsers> _adminPasswordHasher;

    public AuthService(
        IUserService userService,
        IAdminUserRepository adminUserRepository,
        IRefreshTokenRepository refreshTokenRepo,
        IJwtTokenGenerator jwtGenerator,
        IPasswordHasher<User> passwordHasher,
        IPasswordHasher<AdminUsers> adminPasswordHasher)
    {
        _userService = userService;
        _adminUserRepository = adminUserRepository;
        _refreshTokenRepo = refreshTokenRepo;
        _jwtGenerator = jwtGenerator;
        _passwordHasher = passwordHasher;
        _adminPasswordHasher = adminPasswordHasher;
    }

    public async Task<LoginResponseDto> LoginAsync(string username, string password)
    {
        var user = await _userService.GetByUsernameAsync(username);
        if (user != null && user.Password != null)
        {
            var verify = _passwordHasher.VerifyHashedPassword(
                user, user.Password, password);

            if (verify != PasswordVerificationResult.Failed)
            {
                return await IssueTokensForUser(user);
            }
        }

        var adminUser = await _adminUserRepository.GetByUsernameAsync(username);
        if (adminUser == null ||
            string.IsNullOrEmpty(adminUser.PasswordHash) ||
            _adminPasswordHasher.VerifyHashedPassword(
                adminUser, adminUser.PasswordHash, password) == PasswordVerificationResult.Failed)
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        return await IssueTokensForAdmin(adminUser);
    }

    public async Task<LoginResponseDto> RefreshTokenAsync(string rawRefreshToken)
    {
        var hash = _jwtGenerator.HashToken(rawRefreshToken);
        var token = await _refreshTokenRepo.GetByHashAsync(hash)
            ?? throw new UnauthorizedAccessException("Invalid refresh token.");

        if (!token.IsActive)
            throw new UnauthorizedAccessException("Refresh token is expired or revoked.");

        var newRaw = _jwtGenerator.GenerateRefreshToken();
        var newHash = _jwtGenerator.HashToken(newRaw);

        token.RevokedAt = DateTime.UtcNow;
        token.ReplacedByTokenHash = newHash;
        await _refreshTokenRepo.UpdateAsync(token);

        LoginResponseDto response;

        if (token.UserId.HasValue)
        {
            var user = await _userService.GetByIdAsync(token.UserId.Value)
                ?? throw new UnauthorizedAccessException("User not found.");

            await _refreshTokenRepo.AddAsync(new RefreshToken
            {
                TokenHash = newHash,
                UserId = token.UserId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            });

            response = new LoginResponseDto
            {
                Token = _jwtGenerator.GenerateToken(user),
                Expiration = DateTime.UtcNow.AddHours(1),
                RefreshToken = newRaw
            };
        }
        else
        {
            var admin = await _adminUserRepository.GetByIdAsync(token.AdminUserId!.Value)
                ?? throw new UnauthorizedAccessException("Admin not found.");

            await _refreshTokenRepo.AddAsync(new RefreshToken
            {
                TokenHash = newHash,
                AdminUserId = token.AdminUserId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            });

            response = new LoginResponseDto
            {
                Token = _jwtGenerator.GenerateToken(admin),
                Expiration = DateTime.UtcNow.AddHours(1),
                RefreshToken = newRaw
            };
        }

        await _refreshTokenRepo.SaveChangesAsync();
        return response;
    }

    public async Task RevokeAsync(string rawRefreshToken)
    {
        var hash = _jwtGenerator.HashToken(rawRefreshToken);
        var token = await _refreshTokenRepo.GetByHashAsync(hash)
            ?? throw new UnauthorizedAccessException("Invalid refresh token.");

        if (!token.IsActive)
            throw new UnauthorizedAccessException("Token is already expired or revoked.");

        token.RevokedAt = DateTime.UtcNow;
        await _refreshTokenRepo.UpdateAsync(token);
        await _refreshTokenRepo.SaveChangesAsync();
    }

    private async Task<LoginResponseDto> IssueTokensForUser(User user)
    {
        var rawRefresh = _jwtGenerator.GenerateRefreshToken();
        var hash = _jwtGenerator.HashToken(rawRefresh);

        await _refreshTokenRepo.AddAsync(new RefreshToken
        {
            TokenHash = hash,
            UserId = user.UserId,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        });
        await _refreshTokenRepo.SaveChangesAsync();

        return new LoginResponseDto
        {
            Token = _jwtGenerator.GenerateToken(user),
            Expiration = DateTime.UtcNow.AddHours(1),
            RefreshToken = rawRefresh
        };
    }

    private async Task<LoginResponseDto> IssueTokensForAdmin(AdminUsers admin)
    {
        var rawRefresh = _jwtGenerator.GenerateRefreshToken();
        var hash = _jwtGenerator.HashToken(rawRefresh);

        await _refreshTokenRepo.AddAsync(new RefreshToken
        {
            TokenHash = hash,
            AdminUserId = admin.Id,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        });
        await _refreshTokenRepo.SaveChangesAsync();

        return new LoginResponseDto
        {
            Token = _jwtGenerator.GenerateToken(admin),
            Expiration = DateTime.UtcNow.AddHours(1),
            RefreshToken = rawRefresh
        };
    }
}