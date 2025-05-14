using Core.Entities;
using Core.Enums;
using Infrastructure.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace Infrastructure.Persistance.Contexts
{
    public class AppDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Loan> Loans { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new LoanConfiguration());
        }
    }
}
