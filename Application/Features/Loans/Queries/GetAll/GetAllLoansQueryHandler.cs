using Application.DTO.Loan;
using AutoMapper;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Loans.Queries.GetAll
{
    public class GetAllLoansQueryHandler : IRequestHandler<GetAllLoansQuery, List<LoanResponseDto>>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;
        public GetAllLoansQueryHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }
        public async Task<List<LoanResponseDto>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
        {
            var loans = await _loanRepository.GetAllAsync();
            return _mapper.Map<List<LoanResponseDto>>(loans);
        }
    }
}
