using Application.DTO.Loan;
using Application.Exceptions;
using Application.Validations.Loan;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Loans.Queries.GetById
{
    public class GetLoanByIdHandler : IRequestHandler<GetLoanByIdQuery, LoanResponseDto>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetLoanByIdHandler(ILoanRepository loanRepository, IMapper mapper,IHttpContextAccessor httpContextAccessor)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoanResponseDto> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(request.Id)
            ?? throw new NotFoundException("Loan not found.");

            var currentUserId = int.Parse(
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            
            if (userRole == "User" && loan.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to view other users' loans.");
            }

            return _mapper.Map<LoanResponseDto>(loan);
        }

    }
    
}
