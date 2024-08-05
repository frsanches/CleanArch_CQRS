using Banking.Domain.Entities.Customers;

namespace Banking.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task Add(Custormer custormer);

        Task<Custormer> GetByIdAsync(Guid id, bool includeTransactions);
    }
}