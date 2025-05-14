using Application.DTO.Loan;
using Application.Exceptions;
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
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public RejectLoanHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<LoanResponseDto> Handle(RejectLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(request.LoanId);
            if (loan == null)
                throw new NotFoundException("Loan not found");

            loan.Status = LoanStatus.Rejected;
            await _loanRepository.UpdateLoanAsync(loan);

            return _mapper.Map<LoanResponseDto>(loan);
        }
    }
}
