using Core.Entities;
using Infrastructure.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Contexts
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new LoanConfiguration());
        }
    }
}
