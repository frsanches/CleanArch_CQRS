using Banking.Domain.Entities.Customers;

namespace Banking.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);

        Task<Customer?> GetByIdAsync(Guid id);

        Task UpdateCustomerEmailAsync(Customer customer);
    }
}