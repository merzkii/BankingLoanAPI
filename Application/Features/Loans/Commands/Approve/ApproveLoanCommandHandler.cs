using Application.DTO.Loan;
using Application.Interfaces;
using Application.Notifications;
using Application.Notifications.Factories;
using Core.Entities.Notifications.Events;
using MediatR;

namespace Application.Features.Loans.Commands.Approve
{
    public class ApproveLoanHandler : IRequestHandler<ApproveLoanCommand, LoanResponseDto>
    {
        private readonly ILoanService _loanService;
        private readonly INotificationService _notifications;

        public ApproveLoanHandler(ILoanService loanService, INotificationService notifications)
        {
            _loanService = loanService;
            _notifications = notifications;
        }

        public async Task<LoanResponseDto> Handle(ApproveLoanCommand request, CancellationToken cancellationToken)
        {
            var result = await _loanService.ApproveLoanAsync(request.LoanId, cancellationToken);

            var messages = LoanNotificationFactory.ForApproval(new LoanApprovedNotification
            {
                Email = result.UserEmail,
                FullName = result.UserFullName,
                LoanReference = result.Reference,
                ApprovedAmount = result.Amount,
                MonthlyPayment = result.MonthlyPayment,
                TermMonths = result.TermMonths
            });

            await _notifications.SendAsync(messages, cancellationToken);

            return result;
        }
    }
}
