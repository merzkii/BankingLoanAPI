using Application.DTO.Loan;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Loans.Queries.GetById
{
    public class GetLoanByIdQuery : IRequest<LoanResponseDto>
    {
        public int Id { get; set; }
        public GetLoanByIdQuery(int id)
        {
            Id = id;
        }
    }
}
