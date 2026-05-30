using Application.Features.Loans.Commands.Update;
using Core.Entities.Loans;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.Loan
{
    internal class UpdateLoanCommandValidator: AbstractValidator<UpdateLoanCommand>
    {
        public UpdateLoanCommandValidator()
        {
            RuleFor(x => x.LoanId)
                .GreaterThan(0).WithMessage("Invalid loan ID.");

            RuleFor(x => x.LoanData.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");

            RuleFor(x => x.LoanData.Currency)
                .NotEmpty()
                .Must(c => LoanRules.AllowedCurrencies.Contains(c))
                .WithMessage($"Currency must be one of: {string.Join(", ", LoanRules.AllowedCurrencies)}");

            RuleFor(x => x.LoanData.Period)
                .GreaterThan(0).WithMessage("Period must be greater than zero.");

            RuleFor(x => x.LoanData.LoanType)
                .IsInEnum().WithMessage("Invalid loan type.");

            RuleFor(x => x.LoanData.Purpose)
                .NotEmpty().WithMessage("Purpose is required.")
                .MaximumLength(500).WithMessage("Purpose cannot exceed 500 characters.");
        }
    }
}
