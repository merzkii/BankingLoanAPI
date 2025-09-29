using Application.DTO.Loan;
using Application.Validations.Loan;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Loans.Queries.GetById
{
   public class GetLoanByIdHandler : IRequestHandler<GetLoanByIdQuery, LoanResponseDto>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;
        public GetLoanByIdHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<LoanResponseDto> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
        {
            
            var loan = await _loanRepository.GetLoanByIdAsync(request.Id);
            return _mapper.Map<LoanResponseDto>(loan);
            
        }
    }
}
