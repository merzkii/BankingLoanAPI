using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configurations
{
    public class LoanStatusHistoryConfiguration : IEntityTypeConfiguration<LoanStatusHistory>
    {
        public void Configure(EntityTypeBuilder<LoanStatusHistory> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Note)
                .HasMaxLength(1000);

            builder.Property(h => h.ChangedAt)
                .IsRequired();

            builder.Property(h => h.FromStatus)
                .IsRequired();

            builder.Property(h => h.ToStatus)
                .IsRequired();

            builder.HasOne(h => h.Loan)
                .WithMany(l => l.StatusHistory)
                .HasForeignKey(h => h.LoanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(h => h.ChangedBy)
                .WithMany()
                .HasForeignKey(h => h.ChangedByAdminId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
