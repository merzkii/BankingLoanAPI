using Core.Enums;
using System.Reflection.Metadata;

namespace Core.Entities.Loans
{
    public static class LoanRules
    {
        public static readonly IReadOnlyList<string> AllowedCurrencies = new[] { "USD", "EUR", "GEL" };

        public static readonly Dictionary<UserType, Dictionary<LoanType, (decimal Min, decimal Max)>> AmountLimits = new()
        {
            [UserType.Individual] = new()
            {
              { LoanType.QuickLoan,   (100,    5_000)  },
              { LoanType.CarLoan,     (5_000,  50_000) },
              { LoanType.Installment, (1_000,  20_000) },
            },

            [UserType.Entrepreneur] = new()
            {
              { LoanType.QuickLoan,   (500,     20_000)  },
              { LoanType.CarLoan,     (10_000,  100_000) },
              { LoanType.Installment, (5_000,   75_000)  },
            },

            [UserType.Company] = new()
            {
              { LoanType.QuickLoan,   (1_000,   50_000)  },
              { LoanType.CarLoan,     (20_000,  300_000) },
              { LoanType.Installment, (10_000,  500_000) },
            },
        };

        public static readonly Dictionary<UserType, Dictionary<LoanType, int>> MaxPeriodMonths = new()
        {
            [UserType.Individual] = new()
            {
              { LoanType.QuickLoan,   12 },
              { LoanType.CarLoan,     84 },
              { LoanType.Installment, 60 },
            },

            [UserType.Entrepreneur] = new()
            {
              { LoanType.QuickLoan,   24 },
              { LoanType.CarLoan,     96 },
              { LoanType.Installment, 84 },
            },

            [UserType.Company] = new()
            {
              { LoanType.QuickLoan,   36 },
              { LoanType.CarLoan,     120 },
              { LoanType.Installment, 120 },
            },
        };

        public static decimal GetInterestRate(LoanType loanType, UserType userType) => (loanType, userType) switch
        {
          (LoanType.QuickLoan, UserType.Individual) => 15m,
          (LoanType.QuickLoan, UserType.Entrepreneur) => 12m,
          (LoanType.QuickLoan, UserType.Company) => 9m,

          (LoanType.CarLoan, UserType.Individual) => 8m,
          (LoanType.CarLoan, UserType.Entrepreneur) => 6m,
          (LoanType.CarLoan, UserType.Company) => 4m,

          (LoanType.Installment, UserType.Individual) => 10m,
          (LoanType.Installment, UserType.Entrepreneur) => 8m,
          (LoanType.Installment, UserType.Company) => 6m,

            _ => 10m,
        };

        public static readonly Dictionary<UserType, decimal> MaxDebtToIncomeRatio = new()
        {
          { UserType.Individual,   0.40m }, // 40%
          { UserType.Entrepreneur, 0.50m }, // 50%
          { UserType.Company,      0.60m }, // 60%
        };
    }
}
