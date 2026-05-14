using Application.DTO.Common;
using Application.DTO.Loan;
using MediatR;

namespace Application.Features.Loans.Queries.GetAll
{
    public record GetAllLoansQuery : LoanQueryParameters, IRequest<PagedResult<LoanResponseDto>>;
}
