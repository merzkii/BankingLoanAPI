using Application.DTO.Repayment;
using MediatR;

namespace Application.Features.Repayments.Commands
{
    public record MakeRepaymentCommand(
        int LoanId,
        decimal Amount,
        string? Notes) : IRequest<RepaymentResponseDto>;
}
