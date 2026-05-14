using Application.Exceptions;
using Application.Features.Users.Commands.Delete;
using Application.Interfaces;
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
        private readonly ICurrentUserService _currentUserService;

        public DeleteLoanHandler(ILoanRepository loanRepository, ICurrentUserService currentUserService)
        {
            _loanRepository = loanRepository;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            
            var loan = await _loanRepository.GetLoanByIdAsync(request.LoanId);

            if (loan == null)
            {
                throw new NotFoundException("User not found.");
            }
                
            var canManageAllLoans = _currentUserService.IsAdmin || _currentUserService.IsAccountant;
            var ownsLoan = loan.UserId == _currentUserService.UserId;

            if (!canManageAllLoans && !ownsLoan) 
            {
                throw new ForbiddenException("You do not have permission to delete this loan.");
            }

            if (!canManageAllLoans && loan.Status != Core.Enums.LoanStatus.InProcess) 
            {
                throw new ForbiddenException("Only in-process loans can be deleted.");
            }

            await _loanRepository.DeleteLoanAsync(loan.LoanId);
            await _loanRepository.SaveChangesAsync();

            return loan.LoanId;
        }
    }
}
