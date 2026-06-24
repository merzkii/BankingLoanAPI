namespace Core.Entities.Auth
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string TokenHash { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }

        public string? ReplacedByTokenHash { get; set; }

        public int? UserId { get; set; }
        public int? AdminUserId { get; set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsRevoked => RevokedAt.HasValue;
        public bool IsActive => !IsRevoked && !IsExpired;
    }
}
