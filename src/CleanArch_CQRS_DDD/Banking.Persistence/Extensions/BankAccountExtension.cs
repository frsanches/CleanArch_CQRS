using Banking.Domain.Entities.Accounts;
using Banking.Persistence.Entities;

namespace Banking.Persistence.Extensions
{
    internal static class BankAccountExtension
    {
        internal static BankAccountTable Convert(this BankAccount bankAccount)
        {
            return new()
            {
                BankAccountId = bankAccount.Id,
                CustomerId = bankAccount.CustomerId,
                Balance = bankAccount.Balance
            };
        }
    }
}