using Application.DTO.Loan;
using Application.Interfaces;
using Application.Notifications;
using Application.Notifications.Factories;
using Core.Entities.Notifications.Events;
using MediatR;

namespace Application.Features.Loans.Commands.Reject
{
    public class RejectLoanHandler : IRequestHandler<RejectLoanCommand, LoanResponseDto>
    {
        private readonly ILoanService _loanService;
        private readonly INotificationService _notifications;

        public RejectLoanHandler(ILoanService loanService, INotificationService notifications)
        {
            _loanService = loanService;
            _notifications = notifications;
        }

        public async Task<LoanResponseDto> Handle(RejectLoanCommand request, CancellationToken cancellationToken)
        {
            var result = await _loanService.RejectLoanAsync(request.LoanId, request.Reason, cancellationToken);

            var messages = LoanNotificationFactory.ForRejection(new LoanRejectedNotification
            {
                Email = result.UserEmail,
                FullName = result.UserFullName,
                LoanReference = result.Reference,
                Reason = request.Reason
            });

            await _notifications.SendAsync(messages, cancellationToken);

            return result;
        }
    }
}
