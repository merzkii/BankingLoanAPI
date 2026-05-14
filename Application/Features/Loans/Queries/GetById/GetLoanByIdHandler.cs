using Application.DTO.Loan;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Loans.Queries.GetById
{
    public class GetLoanByIdHandler : IRequestHandler<GetLoanByIdQuery, LoanResponseDto>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUserService _currentUserService;
        public GetLoanByIdHandler(ILoanRepository loanRepository, IMapper mapper,IHttpContextAccessor httpContextAccessor,ICurrentUserService currentUserService)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _currentUserService = currentUserService;
        }

        public async Task<LoanResponseDto> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var loan = await _loanRepository.GetLoanByIdAsync(request.Id)
            ?? throw new NotFoundException("Loan not found.");

            var canViewAllLoans = _currentUserService.IsAdmin || _currentUserService.IsAccountant;
            var ownsLoan = loan.UserId ==  _currentUserService.UserId;

            if (!canViewAllLoans && !ownsLoan)
            {
                throw new ForbiddenException("You are not allowed to view other users' loans.");
            }

            return _mapper.Map<LoanResponseDto>(loan);
        }

    }
    
}
