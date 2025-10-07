using Application.DTO.Loan;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Core.Enums;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Loans.Commands.Approve
{

  
    public class ApproveLoanHandler : IRequestHandler<ApproveLoanCommand, LoanResponseDto>
    {
        private readonly ILoanService _loanService;
        private readonly IMapper _mapper;

        public ApproveLoanHandler(ILoanService loanService, IMapper mapper)
        {
            _loanService = loanService;
            _mapper = mapper;
        }

        public async Task<LoanResponseDto> Handle(ApproveLoanCommand request, CancellationToken cancellationToken)
        {
          return  await _loanService.ApproveLoanAsync(request.LoanId);
        }
    }
}
