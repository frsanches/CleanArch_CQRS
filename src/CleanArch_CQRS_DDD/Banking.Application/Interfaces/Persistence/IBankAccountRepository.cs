using Banking.Domain.Entities.Accounts;

namespace Banking.Application.Interfaces
{
    public interface IBankAccountRepository
    {
        Task Add(BankAccount bankAcount);

        Task<BankAccount> GetByIdAsync(Guid id);
    }
}