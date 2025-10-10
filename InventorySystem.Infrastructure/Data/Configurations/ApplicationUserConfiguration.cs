using InventorySystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace InventorySystem.Infrastructure.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            var users = new[]
            {
                new ApplicationUser
                {
                    Id = "U1",
                    UserName = "admin1@inventory.com",
                    NormalizedUserName = "ADMIN1@INVENTORY.COM",
                    Email = "admin1@inventory.com",
                    NormalizedEmail = "ADMIN1@INVENTORY.COM",
                    EmailConfirmed = true,
                    CreatedAt = DateTime.Now,
                    PasswordHash = hasher.HashPassword(null, "Admin@123")
                },
                new ApplicationUser
                {
                    Id = "U2",
                    UserName = "admin2@inventory.com",
                    NormalizedUserName = "ADMIN2@INVENTORY.COM",
                    Email = "admin2@inventory.com",
                    NormalizedEmail = "ADMIN2@INVENTORY.COM",
                    EmailConfirmed = true,
                    CreatedAt = DateTime.Now,
                    PasswordHash = hasher.HashPassword(null, "Admin@123")
                },
                new ApplicationUser
                {
                    Id = "U3",
                    UserName = "user1@inventory.com",
                    NormalizedUserName = "USER1@INVENTORY.COM",
                    Email = "user1@inventory.com",
                    NormalizedEmail = "USER1@INVENTORY.COM",
                    EmailConfirmed = true,
                    CreatedAt = DateTime.Now,
                    PasswordHash = hasher.HashPassword(null, "User@123")
                },
                new ApplicationUser
                {
                    Id = "U4",
                    UserName = "user2@inventory.com",
                    NormalizedUserName = "USER2@INVENTORY.COM",
                    Email = "user2@inventory.com",
                    NormalizedEmail = "USER2@INVENTORY.COM",
                    EmailConfirmed = true,
                    CreatedAt = DateTime.Now,
                    PasswordHash = hasher.HashPassword(null, "User@123")
                },
                new ApplicationUser
                {
                    Id = "U5",
                    UserName = "user3@inventory.com",
                    NormalizedUserName = "USER3@INVENTORY.COM",
                    Email = "user3@inventory.com",
                    NormalizedEmail = "USER3@INVENTORY.COM",
                    EmailConfirmed = true,
                    CreatedAt = DateTime.Now,
                    PasswordHash = hasher.HashPassword(null, "User@123")
                }
            };

            builder.HasData(users);
        }
    }
}
