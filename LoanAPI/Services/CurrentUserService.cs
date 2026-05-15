using Application.Interfaces;
using System.Security.Claims;

namespace LoanAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true;

        public int UserId
        {
            get
            {
                var claim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);

                if (claim == null || !int.TryParse(claim.Value, out var userId))
                {
                    throw new UnauthorizedAccessException("User identifier claim is missing or invalid.");
                }
                return userId;
            }
        }

        public string? Role => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

        public bool IsAdmin => Role == "Admin";

        public bool IsAccountant => Role == "Accountant";

        public bool IsUser => Role == "User";
    }
}
