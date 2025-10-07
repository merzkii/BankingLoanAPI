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

namespace Application.Features.Loans.Commands.Reject
{
    public class RejectLoanHandler : IRequestHandler<RejectLoanCommand, LoanResponseDto>
    {
        private readonly ILoanService _loanService;
        private readonly IMapper _mapper;

        public RejectLoanHandler(ILoanService loanService)
        {
            _loanService = loanService;
        }


        public async Task<LoanResponseDto> Handle(RejectLoanCommand request, CancellationToken cancellationToken)
        {
            
            return await _loanService.RejectLoanAsync(request.LoanId);
        }
    }
}
