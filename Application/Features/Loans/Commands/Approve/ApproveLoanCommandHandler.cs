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

namespace Application.Features.Loans.Commands.Approve
{

  
    public class ApproveLoanHandler : IRequestHandler<ApproveLoanCommand, LoanResponseDto>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public ApproveLoanHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<LoanResponseDto> Handle(ApproveLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(request.LoanId);
            if (loan == null)
                throw new NotFoundException("Loan not found");

            loan.Status = LoanStatus.Approved;
            await _loanRepository.UpdateLoanAsync(loan);

            return _mapper.Map<LoanResponseDto>(loan);
        }
    }
}
