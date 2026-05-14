using Application.DTO.Loan;
using Core.Enums;

namespace Application.DTO.User
{
    public record UserResponseDto
    {
        public int UserId { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Username { get; init; } = string.Empty;
        public int Age { get; init; }
        public string Email { get; init; } = string.Empty;
        public decimal MonthlyIncome { get; init; }
        public bool IsBlocked { get; init; }
        public List<LoanResponseDto> Loans { get; init; } = new();
        public UserType UserType { get; init; }
    }
}
