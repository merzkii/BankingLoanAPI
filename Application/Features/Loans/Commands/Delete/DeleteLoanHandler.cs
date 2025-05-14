using Application.Exceptions;
using Application.Features.Users.Commands.Delete;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Loans.Commands.DeleteLoan
{
    public class DeleteLoanHandler : IRequestHandler<DeleteLoanCommand, int>
    {
        private readonly ILoanRepository _loanRepository;

        
        public DeleteLoanHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<int> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(request.LoanId);
            if (loan == null)
                throw new NotFoundException("User not found.");

            await _loanRepository.DeleteLoanAsync(loan.LoanId);
            return loan.LoanId;
        }
    }
}
