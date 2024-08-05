namespace Banking.Persistence.Entities
{
    #nullable disable warnings
    internal class DebitTransactionTable
    {
        public Guid DebitTransactionId { get; set; }
        public Guid BankAccountId { get; set; }
        public BankAccountTable BankAccount { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }
}