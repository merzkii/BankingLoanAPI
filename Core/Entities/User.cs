using Core.Enums;

namespace Core.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public decimal MonthlyIncome { get; set; }
        public bool IsBlocked { get; set; } 
        public string? Password { get; set; } 
        public List<Loan>? Loans { get; set; }
        public UserType UserType { get; set; }
    }
}
