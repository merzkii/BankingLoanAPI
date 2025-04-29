using Application.DTO.Loan;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Loans.Commands.CreateLoan
{
    public class CreateLoanHandler : IRequestHandler<CreateLoanCommand, LoanResponseDto>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateLoanHandler(
            ILoanRepository loanRepository,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)

        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        

        public async Task<LoanResponseDto> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null || user.IsBlocked)
                throw new UnauthorizedAccessException("You are not allowed to request a loan.");

            var loan = new Loan
            {
                Amount = request.LoanRequest.Amount,
                Currency = request.LoanRequest.Currency,
                Period = request.LoanRequest.Period,
                LoanType = request.LoanRequest.LoanType,
                Status = LoanStatus.InProcess,
                UserId = user.UserId
            };

            await _loanRepository.AddLoanAsync(loan);

            return new LoanResponseDto
            {
                Id = loan.LoanId,
                Amount = loan.Amount,
                Currency = loan.Currency,
                Period = loan.Period,
                LoanType = loan.LoanType,
                Status = loan.Status
            };
        }

    }
}
