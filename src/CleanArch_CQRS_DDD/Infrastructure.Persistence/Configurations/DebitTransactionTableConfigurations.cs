using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
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

            builder.HasOne<BankAccountTable>()
                .WithMany(p => p.DebitTransactions)
                .HasForeignKey(p => p.DebitTransactionId);
        }
    }
}
