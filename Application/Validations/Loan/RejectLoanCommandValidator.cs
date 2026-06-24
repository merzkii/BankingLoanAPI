using Application.Features.Loans.Commands.Reject;
using FluentValidation;

namespace Application.Validations.Loan
{
    public class RejectLoanCommandValidator : AbstractValidator<RejectLoanCommand>
    {
        public RejectLoanCommandValidator()
        {
            RuleFor(x => x.LoanId)
                .GreaterThan(0)
                .WithMessage("Invalid loan ID");

            RuleFor(x => x.Reason)
                .NotEmpty()
                .WithMessage("Rejection reason is required")
                .MaximumLength(1000)
                .WithMessage("Rejection reason cannot exceed 1000 characters");
        }
    }
}
