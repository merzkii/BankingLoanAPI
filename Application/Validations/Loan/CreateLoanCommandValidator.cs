using Application.Features.Loans.Commands.CreateLoan;
using FluentValidation;

namespace Application.Validations.Loan
{
    public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
    {
        public CreateLoanCommandValidator()
        {
            RuleFor(x => x.LoanRequest.Amount).GreaterThan(0);
            RuleFor(x => x.LoanRequest.Currency).NotEmpty();
            RuleFor(x => x.LoanRequest.Period).GreaterThan(0);
            RuleFor(x => x.LoanRequest.LoanType).IsInEnum();
        }
    }
}
