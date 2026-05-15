using Application.DTO.Loan;
using Application.Interfaces;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Features.Loans.Commands.CreateLoan
{
    public class CreateLoanHandler : IRequestHandler<CreateLoanCommand, LoanResponseDto>
    {
        private readonly ILoanService _loanService;
        private readonly ICurrentUserService _currentUserServicr;

        public CreateLoanHandler(
            ILoanService loanService,
            ICurrentUserService currentUserService
            )
        {
            _loanService = loanService;
            _currentUserServicr = currentUserService;
        }

        public async Task<LoanResponseDto> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {

            return await _loanService.CreateLoanAsync(_currentUserServicr.UserId, request.LoanRequest);
        }
    }
}
