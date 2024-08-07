using Banking.Domain.Entities.Accounts;
using Banking.Persistence.Entities;
using Banking.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Banking.Persistence.IntegrationTests
{
    public class BankAccountRepositoryTests
    {
        private readonly ApplicationDbContext _dbContext;

        public BankAccountRepositoryTests()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString()
                )
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            _dbContext = new ApplicationDbContext(dbOptions.Options);
        }

        [Fact]
        public async Task Should_AddNewBankAccount_WhenValidBankAccount()
        {
            // Arrange
            var customer = CreateNewCustomer();
            var initialAmout = 100;
            var bankAccount = BankAccount.Create(customer.CustomerId, initialAmout);
            var bankAccountRepository = new BankAccountRepository(_dbContext);

            // Act
            await bankAccountRepository.AddAsync(bankAccount);
            _dbContext.ChangeTracker.LazyLoadingEnabled = false;
            var dbBankAccount = _dbContext.BankAccount.Find(bankAccount.Id);

            // Assert
            Assert.NotNull(dbBankAccount);
            Assert.Equal(dbBankAccount.BankAccountId, bankAccount.Id);
            Assert.Equal(dbBankAccount.Balance, bankAccount.Balance);
            Assert.Equal(dbBankAccount.CustomerId, bankAccount.CustomerId);
        }

        private CustomerTable CreateNewCustomer()
        {
            var firstName = "John";
            var lastName = "Doe";
            var email = "john.doe@bank.com";
            var ssn = "416-27-7825";

            var customer = new CustomerTable
            {
                CustomerId = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                SSN = ssn
            };

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return customer;
        }
    }
}