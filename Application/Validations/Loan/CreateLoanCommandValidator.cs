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
       .GreaterThan(0).WithMessage("Amount must be greater than zero.");

            RuleFor(x => x.LoanRequest.Currency)
                .NotEmpty()
                .Must(c => LoanRules.AllowedCurrencies.Contains(c))
                .WithMessage($"Currency must be one of: {string.Join(", ", LoanRules.AllowedCurrencies)}");

            RuleFor(x => x.LoanRequest.Period)
                .GreaterThan(0).WithMessage("Period must be greater than zero.");

            RuleFor(x => x.LoanRequest.LoanType)
                .IsInEnum().WithMessage("Invalid loan type.");

            RuleFor(x => x.LoanRequest.Purpose)
                .NotEmpty().WithMessage("Purpose is required.")
                .MaximumLength(500).WithMessage("Purpose cannot exceed 500 characters.");
        }
    }
}
