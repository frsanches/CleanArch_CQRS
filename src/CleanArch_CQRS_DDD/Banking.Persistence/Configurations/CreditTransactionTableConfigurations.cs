using Banking.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.Persistence.Configurations
{
    internal class CreditTransactionTableConfigurations : IEntityTypeConfiguration<CreditTransactionTable>
    {
        public void Configure(EntityTypeBuilder<CreditTransactionTable> builder) 
        {
            builder.ToTable("CreditTransaction");

            builder.HasKey(b => b.CreditTransactionId);
            builder.Property(b => b.CreditTransactionId).ValueGeneratedNever();

            builder.Property(b => b.Amount)
                .IsRequired();

            builder.Property(b => b.Description)
               .HasMaxLength(50).IsRequired();

            builder.HasOne(p => p.BankAccount)
                .WithMany(p => p.CreditTransactions)
                .HasForeignKey(p => p.BankAccountId)
                .IsRequired();
        }
    }
}