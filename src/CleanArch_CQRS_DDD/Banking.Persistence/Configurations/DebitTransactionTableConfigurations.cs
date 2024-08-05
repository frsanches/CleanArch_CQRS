using Banking.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.Persistence.Configurations
{
    internal class DebitTransactionTableConfigurations : IEntityTypeConfiguration<DebitTransactionTable>
    {
        public void Configure(EntityTypeBuilder<DebitTransactionTable> builder) 
        {
            builder.ToTable("DebitTransaction");

            builder.HasKey(b => b.DebitTransactionId);
            builder.Property(b => b.DebitTransactionId).ValueGeneratedNever();

            builder.Property(b => b.Amount)
                .IsRequired();

            builder.Property(b => b.Description)
               .HasMaxLength(50).IsRequired();

            builder.HasOne(p => p.BankAccount)
                .WithMany(p => p.DebitTransactions)
                .HasForeignKey(p => p.BankAccountId)
                .IsRequired();
        }
    }
}