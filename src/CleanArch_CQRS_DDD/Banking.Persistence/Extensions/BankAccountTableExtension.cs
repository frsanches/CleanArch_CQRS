using Banking.Domain.Entities.Accounts;
using Banking.Persistence.Entities;

namespace Banking.Persistence.Extensions
{
    internal static class BankAccountTableExtension
    {
        internal static BankAccount Convert(this BankAccountTable bankAccountTable)
        {
            return BankAccount.FromDb(bankAccountTable.BankAccountId, bankAccountTable.CustomerId, bankAccountTable.Balance);
        }
    }
}