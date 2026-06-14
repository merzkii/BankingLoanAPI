namespace Application.DTO.Auth
{
    public record RefreshTokenRequestDto
    {
        public string RefreshToken { get; init; } = string.Empty;
    }
}
