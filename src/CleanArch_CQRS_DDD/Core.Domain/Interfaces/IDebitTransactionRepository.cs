using Core.Domain.Entities.Transactions;

namespace Core.Domain.Interfaces
{
    public interface IDebitTransactionRepository
    {
        void Add(DebitTransaction transaction);
    }
}