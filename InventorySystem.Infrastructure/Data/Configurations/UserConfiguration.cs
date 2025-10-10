using InventorySystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Infrastructure.Data.Configurations
{
    internal class UserConfiguration
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.Role)
                   .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                   .IsRequired();

            builder.Property(u => u.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");
        }
    }
}
