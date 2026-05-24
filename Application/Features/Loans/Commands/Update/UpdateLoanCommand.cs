using Application.DTO.Loan;
using MediatR;

namespace Application.Features.Loans.Commands.Update
{
    public record UpdateLoanCommand(int LoanId, LoanRequestDto LoanData) : IRequest<LoanResponseDto>;
}
