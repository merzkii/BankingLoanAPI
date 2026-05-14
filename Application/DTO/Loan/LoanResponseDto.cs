using Core.Enums;

namespace Application.DTO.Loan
{
    public record LoanResponseDto
    {
        public int Id { get; init; }
        public decimal Amount { get; init; }
        public string Currency { get; init; } = string.Empty;
        public int Period { get; init; }
        public LoanType LoanType { get; init; }
        public LoanStatus Status { get; init; }
        public int UserId { get; init; }
    }
}
