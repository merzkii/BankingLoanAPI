using Core.Enums;

namespace Application.DTO.Loan
{
    public record LoanRequestDto
    {
        public decimal Amount { get; init; }
        public string Currency { get; init; } = string.Empty;
        public int Period { get; init; } 
        public LoanType LoanType { get; init; } 
        public string? Purpose { get; init; }
    }
}
