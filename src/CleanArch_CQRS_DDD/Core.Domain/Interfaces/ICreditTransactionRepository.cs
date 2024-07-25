using Core.Domain.Entities.Transactions;

namespace Core.Domain.Interfaces
{
    public interface ICreditTransactionRepository
    {
        void Add(CreditTransaction transaction);
    }
}