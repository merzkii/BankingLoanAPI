using Application.DTO.Loan;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoanService(ILoanRepository loanRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<LoanResponseDto> ApproveLoanAsync(int loanId)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(loanId)
            ?? throw new NotFoundException("Loan not found.");
            var userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            loan.Status = LoanStatus.Approved;
            loan.ApprovedById = userId;

            await _loanRepository.UpdateLoanAsync(loan);
            await _loanRepository.SaveChangesAsync();

            return _mapper.Map<LoanResponseDto>(loan);
        }

        public async Task<LoanResponseDto> RejectLoanAsync(int loanId)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(loanId)
                ?? throw new NotFoundException("Loan not found.");
            var userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            loan.Status = LoanStatus.Rejected;
            loan.RejectedById = userId;

            await _loanRepository.UpdateLoanAsync(loan);
            await _loanRepository.SaveChangesAsync();

            return _mapper.Map<LoanResponseDto>(loan);
        }


    }

}
