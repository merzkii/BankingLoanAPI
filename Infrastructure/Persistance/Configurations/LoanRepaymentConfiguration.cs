using Core.Entities.Loans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class LoanRepaymentConfiguration : IEntityTypeConfiguration<LoanRepayment>
    {
        public void Configure(EntityTypeBuilder<LoanRepayment> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(r => r.RemainingBalanceBefore)
                .HasPrecision(18, 2);

            builder.Property(r => r.RemainingBalanceAfter)
            .HasPrecision(18, 2);

            builder.Property(r => r.Notes)
                .HasMaxLength(500);

            builder.Property(r => r.PaidAt)
                .IsRequired();

            builder.HasOne(r => r.Loan)
                .WithMany(l => l.Repayments)
                .HasForeignKey(r => r.LoanId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
