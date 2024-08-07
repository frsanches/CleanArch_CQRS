using Banking.Application.Interfaces;
using Banking.Domain.Entities.Accounts;
using Banking.Domain.Entities.Transactions;
using Banking.Persistence.Extensions;
using System.Text;

namespace Banking.Persistence.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly ApplicationDbContext _DbContext;

        public BankAccountRepository(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task AddAsync(BankAccount bankAcount)
        {

            _DbContext.BankAccount.Add(bankAcount.Convert());
            _DbContext.CreditTransaction.Add(((CreditTransaction)bankAcount.Transactions.First()).Convert());

            await _DbContext.SaveChangesAsync();
        }

        public async Task<BankAccount?> GetByIdAsync(Guid id)
        {
            var bankAccountTable = await _DbContext.BankAccount.FindAsync(id);

            BankAccount? bankAccount = bankAccountTable?.Convert();

            return bankAccount;
        }
    }
}