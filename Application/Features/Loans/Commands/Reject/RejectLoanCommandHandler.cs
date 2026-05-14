using Application.DTO.Loan;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.Loans.Commands.Reject
{
    public class RejectLoanHandler : IRequestHandler<RejectLoanCommand, LoanResponseDto>
    {
        private readonly ILoanService _loanService;
        private readonly IMapper _mapper;

        public RejectLoanHandler(ILoanService loanService, IMapper mapper)
        {
            _loanService = loanService;
            _mapper = mapper;
        }

        public async Task<LoanResponseDto> Handle(RejectLoanCommand request, CancellationToken cancellationToken)
        {
            return await _loanService.RejectLoanAsync(request.LoanId);
        }
    }
}
