using Application.DTO.Loan;
using MediatR;

namespace Application.Features.Loans.Commands.CreateLoan
{
    public record CreateLoanCommand(LoanRequestDto LoanRequest) : IRequest<LoanResponseDto>;
}
