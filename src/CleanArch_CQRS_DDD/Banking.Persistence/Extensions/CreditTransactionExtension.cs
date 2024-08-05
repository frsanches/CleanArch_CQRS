using Banking.Domain.Entities.Transactions;
using Banking.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Persistence.Extensions
{
    internal static class CreditTransactionExtension
    {
        internal static CreditTransactionTable Convert(this CreditTransaction creditTransaction) 
        {
            return new()
            {
                CreditTransactionId = creditTransaction.Id,
                BankAccountId = creditTransaction.AccountId,
                Amount = creditTransaction.Amount,
                Description = creditTransaction.Description
            };
        }
    }
}