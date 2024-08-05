using Banking.Domain.Entities.Accounts;

namespace Banking.Application.Interfaces
{
    public interface IDebitTransactionRepository
    {
        Task Add(BankAccount transaction);
    }
}