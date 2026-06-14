using Core.Entities;
using Core.Entities.Admins;
using Core.Entities.Auth;
using Core.Entities.Loans;
using Core.Entities.Users;
using Infrastructure.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<AdminUsers> AdminUsers { get; set; }
        public DbSet<LoanStatusHistory> LoanStatusHistories { get; set; }
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new LoanConfiguration());
            modelBuilder.ApplyConfiguration(new AdminUsersConfiguration());
            modelBuilder.ApplyConfiguration(new LoanStatusHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        }
    }
}
