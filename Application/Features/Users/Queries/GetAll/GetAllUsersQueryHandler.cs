using Application.DTO.Loan;
using Application.DTO.User;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetAll
{
    class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponseDto>>
    {
        private readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<UserResponseDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllAsync();
            return users.Select(user => new UserResponseDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName=user.LastName,
                Username = user.Username,
                Email = user.Email,
                UserType = user.UserType,
                IsBlocked = user.IsBlocked,
                Loans=user.Loans.Select(l=>new LoanResponseDto { Id=l.LoanId,
                Amount=l.Amount,
                Currency=l.Currency,
                Period=l.Period,
                LoanType=l.LoanType,
                Status=l.Status,
                }).ToList()
                
                
            }).ToList();

            //return users.Select(user => new UserResponseDto
            //{
            //    UserId = user.UserId,
            //    Username = user.Username,
            //    Email = user.Email,
            //    UserRole = user.UserRole,
            //    IsBlocked = user.IsBlocked
            //}).ToList();
        }
    }
}

