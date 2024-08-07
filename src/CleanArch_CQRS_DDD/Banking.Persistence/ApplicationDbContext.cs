using Banking.Persistence.Entities;
using Banking.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Banking.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            this.ChangeTracker.LazyLoadingEnabled = false; 
        }

        internal DbSet<CustomerTable> Customers { get; set; }
        internal DbSet<BankAccountTable> BankAccount { get; set; }
        internal DbSet<CreditTransactionTable> CreditTransaction { get; set; }
        internal DbSet<DebitTransactionTable> DebitTransaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.SeedData();
        }
    }
}