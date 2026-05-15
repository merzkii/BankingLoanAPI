using Core.Enums;

namespace Core.Entities
{
    public static class LoanRules
    {
        public static readonly IReadOnlyList<string> AllowedCurrencies = new[] { "USD", "EUR", "GEL" };

        public static readonly Dictionary<LoanType, (decimal Min, decimal Max)> AmountLimits = new()
    {
        { LoanType.QuickLoan,   (100,    5_000)  },
        { LoanType.CarLoan,     (5_000,  50_000) },
        { LoanType.Installment, (1_000,  20_000) },
    };

        public static readonly Dictionary<LoanType, int> MaxPeriodMonths = new()
    {
        { LoanType.QuickLoan,   12  },
        { LoanType.CarLoan,     84  },
        { LoanType.Installment, 60  },
    };

        public static decimal GetInterestRate(LoanType type) => type switch
        {
            LoanType.QuickLoan => 15m,
            LoanType.CarLoan => 8m,
            LoanType.Installment => 10m,
            _ => 10m,
        };

        public const decimal MaxDebtToIncomeRatio = 0.4m;
    }
}
