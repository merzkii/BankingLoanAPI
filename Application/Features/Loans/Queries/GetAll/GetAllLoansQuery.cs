using Application.DTO.Loan;
using MediatR;

namespace Application.Features.Loans.Queries.GetAll
{
    public class GetAllLoansQuery : IRequest<List<LoanResponseDto>>
    {

    }
    
}
