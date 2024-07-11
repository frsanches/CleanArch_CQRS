namespace Core.Domain.Entities.Transactions
{
    public interface ITransaction
    {
        Double Amount { get; }
        string Description { get; }
        DateTime TransactionDate { get; }
    }
}