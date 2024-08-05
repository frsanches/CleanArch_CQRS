using Banking.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.Persistence.Configurations
{
    internal class BankAccountTableConfigurations : IEntityTypeConfiguration<BankAccountTable>
    {
        public void Configure(EntityTypeBuilder<BankAccountTable> builder)
        {
            builder.ToTable("BankAccount");

            builder.HasKey(b => b.BankAccountId);
            builder.Property(b => b.BankAccountId).ValueGeneratedNever();

            builder.Property(b => b.Balance)
                .IsRequired();
        }
    }
}