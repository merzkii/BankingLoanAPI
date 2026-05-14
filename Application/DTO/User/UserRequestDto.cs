namespace Application.DTO.User
{
    public record UserRequestDto
    {
        public string UserId { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Username { get; init; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
