using Banking.Domain.Entities.Customers;
using Banking.Persistence.Entities;
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

            var custormer = Customer.Create(firstName, lastName, email, ssn);
            var customerRepository = new CustomerRepository(_dbContext);

            // Act
            await customerRepository.AddAsync(custormer.Value!);

            var dbCustomer = _dbContext.Customers.Find(custormer.Value!.Id);

            Assert.Equal(dbCustomer?.CustomerId, custormer.Value!.Id);
        }

        [Fact]
        public async Task Should_GetCustomerById_WhenCustomerExists()
        {
            // Arrange
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

            var customerRepository = new CustomerRepository(_dbContext);

            //act
            var expectedCustomer = await customerRepository.GetByIdAsync(customer.CustomerId);

            //assert
            Assert.NotNull(expectedCustomer);
            Assert.Equal(expectedCustomer.Id, customer.CustomerId);
            Assert.Equal(expectedCustomer.FirstName, firstName);
            Assert.Equal(expectedCustomer.LastName, lastName);
            Assert.Equal(expectedCustomer.Email, email);
            Assert.Equal(expectedCustomer.SSN.Value, ssn);
        }
    }
}
