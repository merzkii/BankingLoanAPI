using Application.Features.Loans.Commands.CreateLoan;
using Core.Entities;
using FluentValidation;

namespace Application.Validations.Loan
{
    public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
    {
        public CreateLoanCommandValidator()
        {
            RuleFor(x => x.LoanRequest.Amount)
                .GreaterThan(0)
                .Must((cmd, amount) =>
                {
                    var (min, max) = LoanRules.AmountLimits[cmd.LoanRequest.LoanType];
                    return amount >= min && amount <= max;
                })
                .WithMessage(cmd =>
                {
                    var (min, max) = LoanRules.AmountLimits[cmd.LoanRequest.LoanType];
                    return $"Amount for {cmd.LoanRequest.LoanType} must be between {min} and {max}";
                });

            RuleFor(x => x.LoanRequest.Currency)
                .Must(c => LoanRules.AllowedCurrencies.Contains(c))
                .WithMessage($"Currency must be one of: {string.Join(", ", LoanRules.AllowedCurrencies)}");

            RuleFor(x => x.LoanRequest.Period)
                .GreaterThan(0)
                .Must((cmd, period) => period <= LoanRules.MaxPeriodMonths[cmd.LoanRequest.LoanType])
                .WithMessage(cmd => $"Max period for {cmd.LoanRequest.LoanType} is {LoanRules.MaxPeriodMonths[cmd.LoanRequest.LoanType]} months.");

            RuleFor(x => x.LoanRequest.LoanType)
                .IsInEnum();

            RuleFor(x => x.LoanRequest.Purpose)
                .NotEmpty()
                .MaximumLength(500);
        }
    }
}
