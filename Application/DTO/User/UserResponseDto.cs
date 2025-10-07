using Application.DTO.Loan;
using Core.Enums;

namespace Application.DTO.User
{
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public decimal MonthlyIncome { get; set; }
        public bool IsBlocked { get; set; } = false;
        public string Password { get; set; }
        public List<LoanResponseDto> Loans { get; set; } = new ();
        public UserType UserType { get; set; }
    }
}
