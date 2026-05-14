using Application.DTO.Common;
using Application.DTO.Loan;
using AutoMapper;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Loans.Queries.GetAll
{
    public class GetAllLoansQueryHandler : IRequestHandler<GetAllLoansQuery, PagedResult<LoanResponseDto>>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public GetAllLoansQueryHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<LoanResponseDto>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            
            var (loans, totalCount) = await _loanRepository.GetPagedAsync(request);
            var items = _mapper.Map<List<LoanResponseDto>>(loans);

            return new PagedResult<LoanResponseDto>(items, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
