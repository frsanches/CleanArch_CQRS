namespace Infrastructure.Persistence.Entities
{
    #nullable disable warnings
    internal class CustomerTable
    {
        internal Guid CustomerId { get; set; }
        internal string FirstName { get; set; }
        internal string LastName { get; set; }
        internal string Email { get; set; }
        internal BankAccountTable BankAccount { get; set; }
    }
}
