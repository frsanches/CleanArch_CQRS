namespace Infrastructure.Persistence.Entities
{
    #nullable disable warnings
    internal class CreditTransactionTable
    {
        public Guid CreditTransactionId { get; set; }
        public Guid BankAccountId { get; set; }
        public BankAccountTable BankAccount { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }
}
