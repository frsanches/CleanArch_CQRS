using Banking.Application.Interfaces;
using Banking.Domain.Entities.Customers;
using Banking.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Banking.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Customer customer)
        {
            _dbContext.Customers.Add(customer.Convert());

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var dbCustomer = await _dbContext.Customers.FindAsync(id);

            return dbCustomer?.Convert();
        }

        public async Task UpdateCustomerEmailAsync(Customer customer)
        {
            await _dbContext.Customers.Where(x => x.CustomerId == customer.Id)
                .ExecuteUpdateAsync(update => update.SetProperty(c => c.Email, customer.Email));
        }
    }
}