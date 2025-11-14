using InventorySystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(c => c.CustomerId);

            builder.Property(c => c.Email).HasMaxLength(100);
            builder.Property(c => c.Phone).HasMaxLength(50);
            builder.Property(c => c.Address).HasMaxLength(255);
            builder.Property(c => c.Name).HasMaxLength(50);

        }
    }
}
