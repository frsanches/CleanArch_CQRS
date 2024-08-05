using Banking.Domain.Entities.Accounts;

namespace Banking.Application.Interfaces
{
    public interface IBankAccountRepository
    {
        Task AddAsync(BankAccount bankAcount);

        Task<BankAccount?> GetByIdAsync(Guid id);
    }
}