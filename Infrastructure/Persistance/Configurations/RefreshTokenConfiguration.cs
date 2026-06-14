using Core.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(rt => rt.Id);
            builder.Property(rt => rt.TokenHash).IsRequired().HasMaxLength(128);
            builder.HasIndex(rt => rt.TokenHash).IsUnique();
            builder.Property(rt => rt.ReplacedByTokenHash).HasMaxLength(128);
        }
    }
}
