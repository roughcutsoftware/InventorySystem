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

           // builder.HasData(
           //    new Customer { CustomerId = 1, Name = "Ahmed Ali", Email = "ahmed@example.com", Phone = "01011112222", Address = "Cairo, Egypt" },
           //    new Customer { CustomerId = 2, Name = "Sara Hassan", Email = "sara@example.com", Phone = "01033334444", Address = "Giza, Egypt" },
           //    new Customer { CustomerId = 3, Name = "Mona Salah", Email = "mona@example.com", Phone = "01055556666", Address = "Alexandria, Egypt" },
           //    new Customer { CustomerId = 4, Name = "Omar Fathy", Email = "omar@example.com", Phone = "01077778888", Address = "Tanta, Egypt" },
           //    new Customer { CustomerId = 5, Name = "Khaled Nasser", Email = "khaled@example.com", Phone = "01099990000", Address = "Aswan, Egypt" }
           //);
        }
    }
}
