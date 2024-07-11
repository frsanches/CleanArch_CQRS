using Core.Domain.Entities.Transactions;

namespace Core.Domain.Entities.Accounts
{
    public class BankAcount : IEntity
    {
        private readonly List<ITransaction> _transactions = new List<ITransaction>();
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public double Balance { get; set; }
        public IReadOnlyList<ITransaction> Transactions => _transactions.ToList();

        private BankAcount(Guid customerId, double amount)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Balance = amount;

            _transactions.Add(new CreditTransaction(Id, amount));
        }

        public ITransaction GetMostRecentTransaction
        {
            get { return _transactions.Last(); }
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("The amount should be superior to 0 balance.");

            Balance += amount;

            _transactions.Add(new CreditTransaction(Id, amount));
        }

        public void Withdraw(double amount)
        {
            if (amount > Balance)
                throw new InvalidOperationException("The amount is higher than the account balance.");

            Balance -= amount;

            _transactions.Add(new DebitTransaction(Id, amount));
        }

        public static BankAcount Create(Guid customerId, double amount)
        {
            ArgumentNullException.ThrowIfNull(customerId, nameof(customerId));

            if (amount < 10)
                throw new InvalidOperationException("You should deposit minimun 10$ to create an account.");

            return new BankAcount(customerId, amount);
        }
    }
}