using Application.DTO.Loan;
using MediatR;

namespace Application.Features.Loans.Queries.GetById
{
    public record GetLoanByIdQuery(int Id) : IRequest<LoanResponseDto>;
}
