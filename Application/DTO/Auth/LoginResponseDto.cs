namespace Application.DTO.Auth
{
    public record LoginResponseDto
    {
        public string Token { get; init; } = string.Empty;
        public DateTime Expiration { get; init; }
    }
}
