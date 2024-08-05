namespace Banking.Domain.Entities.Transactions
{
    public class DebitTransaction : IEntity, ITransaction
    {
        public Guid Id { get; private set; }

        public Guid AccountId { get; private set; }

        public double Amount { get; private set; }

        public string Description => "Debit Transaction";

        public DateTime TransactionDate { get; private set; }

        public DebitTransaction(Guid accountId, double amount)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            Amount = amount;
            TransactionDate = DateTime.UtcNow;
        }
    }
}