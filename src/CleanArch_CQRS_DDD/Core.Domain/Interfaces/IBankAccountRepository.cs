using Core.Domain.Entities.Accounts;

namespace Core.Domain.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<BankAccount> GetByIdAsync(Guid id);
        void add(BankAccount bankAcount);
    }
}
