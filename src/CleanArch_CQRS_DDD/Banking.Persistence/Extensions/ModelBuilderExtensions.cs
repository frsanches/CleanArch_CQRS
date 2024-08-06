using Banking.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banking.Persistence.Extensions
{
    internal static class ModelBuilderExtensions
    {
        internal static void SeedData(this ModelBuilder modelBuilder)
        {
            var customerId = Guid.NewGuid();
            var bankAccountId = Guid.NewGuid();
            var initialAmount = 1000;

            modelBuilder.Entity<CustomerTable>().HasData(
                new CustomerTable
                {
                    CustomerId = customerId,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@banking.com",
                    SSN = "545-40-9491"
                });

            modelBuilder.Entity<BankAccountTable>().HasData(
                new BankAccountTable
                {
                    BankAccountId = bankAccountId,
                    Balance = initialAmount,
                    CustomerId = customerId,
                });

            modelBuilder.Entity<CreditTransactionTable>().HasData(
                new CreditTransactionTable
                {
                    CreditTransactionId = Guid.NewGuid(),
                    BankAccountId = bankAccountId,
                    Amount = initialAmount,
                    Description = "Credit Transaction"
                });
        }
    }
}