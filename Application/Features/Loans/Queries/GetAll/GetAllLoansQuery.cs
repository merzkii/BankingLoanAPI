using Application.DTO.Loan;
using MediatR;

namespace Application.Features.Loans.Queries.GetAll
{
    public record GetAllLoansQuery : IRequest<List<LoanResponseDto>>
    {

    }
    
}
