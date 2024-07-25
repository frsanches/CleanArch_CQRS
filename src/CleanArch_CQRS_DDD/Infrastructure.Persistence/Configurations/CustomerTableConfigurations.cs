using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class CustomerTableConfigurations : IEntityTypeConfiguration<CustomerTable>
    {
        public void Configure(EntityTypeBuilder<CustomerTable> builder) 
        {
            builder.ToTable("Customer");

            builder.HasKey(b => b.CustomerId);
            builder.Property(b => b.CustomerId).ValueGeneratedNever();

            builder.Property(b => b.FirstName)
                .HasMaxLength(50).IsRequired();

            builder.Property(b => b.LastName)
               .HasMaxLength(50).IsRequired();

            builder.Property(b => b.Email)
                .HasMaxLength(50).IsRequired();
        }
    }
}
