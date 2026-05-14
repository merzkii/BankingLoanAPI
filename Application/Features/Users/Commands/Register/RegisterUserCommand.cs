using Application.DTO.Auth;
using Core.Enums;
using MediatR;

namespace Application.Features.Users.Commands.Register
{
    public record RegisterUserCommand:IRequest<LoginResponseDto>
    {
        public string FirstName { get; init ; } = string.Empty; 
        public string LastName { get; init; } = string.Empty;   
        public string Username { get; init; } = string.Empty;
        public int Age { get; init; }
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;   
        public decimal MonthlyIncome { get; init; }
        public UserType UserType { get; init; }
    }
}
