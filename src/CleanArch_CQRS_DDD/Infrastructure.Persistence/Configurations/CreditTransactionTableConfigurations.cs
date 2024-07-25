using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
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

            builder.HasOne<BankAccountTable>()
                .WithMany(p => p.CreditTransactions)
                .HasForeignKey(p => p.CreditTransactionId);
        }
    }
}
