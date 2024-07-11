using Core.Domain.Entities.Accounts;

namespace Domain.Tests
{
    public class BankAccountTests
    {
        [Fact]
        public void BankAccount_Create_ShouldReturnObjectInstance()
        {
            var (customerId, initialAmmount) = Init();

            var bankAccount = BankAcount.Create(customerId, initialAmmount);

            Assert.IsType<BankAcount>(bankAccount);
            Assert.Equal(initialAmmount, bankAccount.Balance);
            Assert.Equal(customerId, bankAccount.CustomerId);
        }

        [Fact]
        public void BankAccount_Create_ShouldThrowAnException()
        {
            var custormerId = Guid.NewGuid();
            var initialAmmount = 5;

            Assert.Throws<InvalidOperationException>(() => BankAcount.Create(custormerId, initialAmmount));
        }

        [Fact]
        public void BankAccount_Deposit_ShouldUpdateTheBalance()
        {
            var (customerId, initialAmmount) = Init();

            var bankAccount = BankAcount.Create(customerId, initialAmmount);
            bankAccount.Deposit(100);


            Assert.Equal(120, bankAccount.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void BankAccount_DepositAmountInferiorOrEqualZero_ShouldThrowAnException(double amount)
        {
            var (customerId, initialAmmount) = Init();

            var bankAccount = BankAcount.Create(customerId, initialAmmount);

            var ex = Assert.Throws<InvalidOperationException>(() => bankAccount.Deposit(amount));
        }

        [Fact]
        public void BankAccount_Withdraw_ShouldUpdateTheBalance()
        {
            var (customerId, initialAmmount) = Init();

            var bankAccount = BankAcount.Create(customerId, initialAmmount);
            bankAccount.Withdraw(10);


            Assert.Equal(10, bankAccount.Balance);
        }

        [Fact]
        public void BankAccount_WithdrawAmmountSuperiorToAccountBalance_ShouldThrowAnException()
        {
            var (customerId, initialAmmount) = Init();

            var bankAccount = BankAcount.Create(customerId, initialAmmount);

            Assert.Throws<InvalidOperationException>(() => bankAccount.Withdraw(30));
        }

        [Fact]
        public void BankAccount_Deposit_ShouldReturnLastCreditTransaction()
        {
            var (customerId, initialAmmount) = Init();

            var bankAccount = BankAcount.Create(customerId, initialAmmount);
            bankAccount.Deposit(100);


            Assert.Equal(100, bankAccount.GetMostRecentTransaction.Amount);
            Assert.Equal(DateTime.UtcNow.Date, bankAccount.GetMostRecentTransaction.TransactionDate.Date);
            Assert.StartsWith("Credit", bankAccount.GetMostRecentTransaction.Description, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void BankAccount_Withdraw_ShouldReturnLastDebitTransaction()
        {
            var (customerId, initialAmmount) = Init();

            var bankAccount = BankAcount.Create(customerId, initialAmmount);
            bankAccount.Withdraw(10);


            Assert.Equal(10, bankAccount.GetMostRecentTransaction.Amount);
            Assert.Equal(DateTime.UtcNow.Date, bankAccount.GetMostRecentTransaction.TransactionDate.Date);
            Assert.StartsWith("Debit", bankAccount.GetMostRecentTransaction.Description, StringComparison.OrdinalIgnoreCase);
        }

        private (Guid Guid, double InitialAmount) Init()
        {
            return (Guid.NewGuid(), 20);
        }
    }
}