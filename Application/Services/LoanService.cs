using Application.DTO.Loan;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;

namespace Application.Services
{
    public class LoanService: ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;
        public LoanService(ILoanRepository loanRepository,IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }
        public async Task<LoanResponseDto> ApproveLoanAsync(int loanId)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(loanId)
            ?? throw new NotFoundException("Loan not found.");

            loan.Status = LoanStatus.Approved;

            await _loanRepository.UpdateLoanAsync(loan);
            await _loanRepository.SaveChangesAsync();

            return _mapper.Map<LoanResponseDto>(loan);
        }

        public async Task<LoanResponseDto> RejectLoanAsync(int loanId)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(loanId)
                ?? throw new NotFoundException("Loan not found.");

            loan.Status = LoanStatus.Rejected;

            await _loanRepository.UpdateLoanAsync(loan);
            await _loanRepository.SaveChangesAsync();

            return _mapper.Map<LoanResponseDto>(loan);
        }


    }
    
}
