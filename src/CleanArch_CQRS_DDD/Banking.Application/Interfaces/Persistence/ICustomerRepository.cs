using Banking.Domain.Entities.Customers;

namespace Banking.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Custormer custormer);

        Task<Custormer?> GetByIdAsync(Guid id);
    }
}