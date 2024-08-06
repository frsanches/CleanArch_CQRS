using Banking.Domain.Entities.Customers;
using Banking.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Banking.Persistence.IntegrationTests
{
    public class CustomerRepositoryTests
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepositoryTests()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString()
                );

            _dbContext = new ApplicationDbContext(dbOptions.Options);
        }

        [Fact]
        public async Task Should_AddNewCustomer_WhenValidCustomer()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";
            var email = "john.doe@bank.com";
            var ssn = "416-27-7825";

            var custormer = Custormer.Create(firstName, lastName, email, ssn);
            var customerRepository = new CustomerRepository(_dbContext);

            // Act
            await customerRepository.AddAsync(custormer);

            var dbCustomer = _dbContext.Customers.Find(custormer.Id);

            Assert.Equal(dbCustomer?.CustomerId, custormer.Id);
        }
    }
}
