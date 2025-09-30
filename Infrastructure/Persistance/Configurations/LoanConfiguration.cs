using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configurations
{
    public class LoanConfiguration: IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasKey(l => l.LoanId);
            builder.Property(l => l.Amount).IsRequired().HasPrecision(18, 2);
            builder.Property(l => l.Currency).IsRequired().HasMaxLength(3);
            builder.Property(l => l.Period).IsRequired();
            builder.Property(l => l.Status).IsRequired().HasDefaultValue(LoanStatus.InProcess);

           
            builder.HasOne(l => l.User)
                   .WithMany(u => u.Loans)
                   .HasForeignKey(l => l.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.ApprovedBy)
            .WithMany()
            .HasForeignKey(l => l.ApprovedById)
            .OnDelete(DeleteBehavior.Restrict);

           
            builder.HasOne(l => l.RejectedBy)
                .WithMany()
                .HasForeignKey(l => l.RejectedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
