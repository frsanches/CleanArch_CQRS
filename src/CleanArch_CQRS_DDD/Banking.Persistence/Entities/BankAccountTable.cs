namespace Banking.Persistence.Entities
{
    #nullable disable warnings
    internal class BankAccountTable
    {
        public Guid BankAccountId { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerTable Customer { get; set; }
        public double Balance { get; set; }
        public ICollection<CreditTransactionTable> CreditTransactions { get; set; }
        public ICollection<DebitTransactionTable> DebitTransactions { get; set; }
    }
}