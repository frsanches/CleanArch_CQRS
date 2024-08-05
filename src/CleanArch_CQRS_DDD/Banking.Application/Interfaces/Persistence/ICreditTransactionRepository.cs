using Banking.Domain.Entities.Accounts;

namespace Banking.Application.Interfaces
{
    public interface ICreditTransactionRepository
    {
        Task Add(BankAccount transaction);
    }
}