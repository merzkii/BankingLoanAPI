using Core.Entities.Admins;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class AdminUsersConfiguration : IEntityTypeConfiguration<AdminUsers>
    {
        public void Configure(EntityTypeBuilder<AdminUsers> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Surname)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasIndex(a => a.Email)
                .IsUnique();

            builder.Property(a => a.Username)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(a => a.Username)
                .IsUnique();

            builder.Property(a => a.PasswordHash)
                .IsRequired();

            builder.Property(a => a.Role)
                .IsRequired();
        }
    }
}

