using Application.DTO.Loan;
using FluentValidation;

namespace Application.Validations.Loan
{
    public class LoanRequestValidator : AbstractValidator<LoanRequestDto>
    {
        public LoanRequestValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Currency).NotEmpty();
            RuleFor(x => x.Period).GreaterThan(0);
            RuleFor(x => x.LoanType).IsInEnum();
        }
    }
}
