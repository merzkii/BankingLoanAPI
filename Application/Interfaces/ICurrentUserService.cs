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
