using InventorySystem.Core.Entities;
using InventorySystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InventorySystem.Infrastructure.Seeding
{
    public static class DbSeeder
    {
        public static async Task InitializeAsync(
           AppDBContext context,
           UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           ILogger logger)
        {
            try
            {
                await SeedRolesAsync(roleManager, logger);
                var users = await SeedUsersAsync(userManager, logger);
                await SeedAppTablesAsync(context, logger, users);

                logger.LogInformation("✅ Database seeding completed successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "❌ Error occurred while seeding the database.");
                throw;
            }
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, ILogger logger)
        {
            string[] roles = ["Admin", "User"];
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    logger.LogInformation($"✅ Role '{role}' created.");
                }
            }
        }

        private static async Task<Dictionary<string, string>> SeedUsersAsync(UserManager<ApplicationUser> userManager, ILogger logger)
        {
            var users = new List<ApplicationUser>
            {
                new() { UserName = "admin1", Email = "admin1@sys.com", EmailConfirmed = true },
                new() { UserName = "user1", Email = "user1@sys.com", EmailConfirmed = true },
                new() { UserName = "user2", Email = "user2@sys.com", EmailConfirmed = true },
                new() { UserName = "user3", Email = "user3@sys.com", EmailConfirmed = true },
                new() { UserName = "user4", Email = "user4@sys.com", EmailConfirmed = true },
            };

            var userIds = new Dictionary<string, string>();

            foreach (var user in users)
            {
                var existing = await userManager.FindByEmailAsync(user.Email);
                if (existing == null)
                {
                    var result = await userManager.CreateAsync(user, "Admin@123");
                    if (result.Succeeded)
                    {
                        var role = user.Email.StartsWith("admin") ? "Admin" : "User";
                        await userManager.AddToRoleAsync(user, role);
                        logger.LogInformation($"✅ User '{user.UserName}' created with role '{role}'.");
                        userIds[user.UserName] = user.Id;
                    }
                }
                else
                {
                    userIds[user.UserName] = existing.Id;
                }
            }

            return userIds;
        }

        private static async Task SeedAppTablesAsync(AppDBContext context, ILogger logger, Dictionary<string, string> users)
        {


            // Suppliers
            if (!await context.Suppliers.AnyAsync())
            {
                await context.Suppliers.AddRangeAsync(
                    new Supplier { Name = "TechVision", Email = "contact@techvision.com", Phone = "0101001001", Address = "Cairo" },
                    new Supplier { Name = "SafeZone", Email = "info@safezone.com", Phone = "0101001002", Address = "Alexandria" },
                    new Supplier { Name = "NetLink", Email = "sales@netlink.com", Phone = "0101001003", Address = "Giza" },
                    new Supplier { Name = "CamPro", Email = "support@campro.com", Phone = "0101001004", Address = "Mansoura" },
                    new Supplier { Name = "SecureMax", Email = "info@securemax.com", Phone = "0101001005", Address = "Tanta" }
                );
                await context.SaveChangesAsync();
                logger.LogInformation("✅ Suppliers seeded.");
            }



            // Customers
            if (!await context.Customers.AnyAsync())
            {
                await context.Customers.AddRangeAsync(
                    new Customer { Name = "Ali Hassan", Email = "ali@example.com", Phone = "0101000001", Address = "Cairo" },
                    new Customer { Name = "Sara Ahmed", Email = "sara@example.com", Phone = "0101000002", Address = "Giza" },
                    new Customer { Name = "Omar Khaled", Email = "omar@example.com", Phone = "0101000003", Address = "Alexandria" },
                    new Customer { Name = "Mona Youssef", Email = "mona@example.com", Phone = "0101000004", Address = "Mansoura" },
                    new Customer { Name = "Hassan Nabil", Email = "hassan@example.com", Phone = "0101000005", Address = "Tanta" }
                );
                await context.SaveChangesAsync();
                logger.LogInformation("✅ Customers seeded.");
            }

            // Categories
            if (!await context.Categories.AnyAsync())
            {
                await context.Categories.AddRangeAsync(
                    new Category { Name = "Cameras", Description = "Security and surveillance cameras" },
                    new Category { Name = "Recorders", Description = "NVR/DVR systems" },
                    new Category { Name = "Accessories", Description = "Cables and mounts" },
                    new Category { Name = "Networking", Description = "Routers and switches" },
                    new Category { Name = "Storage", Description = "Hard drives and SSDs" }
                );
                await context.SaveChangesAsync();
                logger.LogInformation("✅ Categories seeded.");
            }

            // Products
            if (!await context.Products.AnyAsync())
            {
                var cats = await context.Categories.ToListAsync();
                var sups = await context.Suppliers.ToListAsync();

                await context.Products.AddRangeAsync(
                    new Product { Name = "IP Camera 1080p", QuantityInStock = 25, UnitPrice = 1500, CostPrice = 1200, ReorderLevel = 5, CreatedAt = DateTime.Now, CategoryId = cats[0].CategoryId, SupplierId = sups[3].SupplierId },
                    new Product { Name = "NVR 8 Channel", QuantityInStock = 10, UnitPrice = 3500, CostPrice = 3000, ReorderLevel = 3, CreatedAt = DateTime.Now, CategoryId = cats[1].CategoryId, SupplierId = sups[4].SupplierId },
                    new Product { Name = "HDMI Cable 5m", QuantityInStock = 100, UnitPrice = 150, CostPrice = 80, ReorderLevel = 20, CreatedAt = DateTime.Now, CategoryId = cats[2].CategoryId, SupplierId = sups[2].SupplierId },
                    new Product { Name = "Router TP-Link AX1800", QuantityInStock = 15, UnitPrice = 1800, CostPrice = 1450, ReorderLevel = 4, CreatedAt = DateTime.Now, CategoryId = cats[3].CategoryId, SupplierId = sups[2].SupplierId },
                    new Product { Name = "WD Purple 2TB", QuantityInStock = 20, UnitPrice = 900, CostPrice = 700, ReorderLevel = 5, CreatedAt = DateTime.Now, CategoryId = cats[4].CategoryId, SupplierId = sups[0].SupplierId }
                );
                await context.SaveChangesAsync();
                logger.LogInformation("✅ Products seeded.");
            }

            // Purchases
            if (!await context.Purchases.AnyAsync())
            {
                var purchases = new List<Purchase>
                {
                    new Purchase { SupplierId = 1, PurchaseDate = DateTime.Now.AddDays(-12), TotalAmount = 2500, Status = "Completed", CreatedById = users["admin1"] },
                    new Purchase { SupplierId = 2, PurchaseDate = DateTime.Now.AddDays(-10), TotalAmount = 1900, Status = "Completed", CreatedById = users["user1"] },
                    new Purchase { SupplierId = 3, PurchaseDate = DateTime.Now.AddDays(-7), TotalAmount = 1600, Status = "Pending", CreatedById = users["user2"] },
                    new Purchase { SupplierId = 4, PurchaseDate = DateTime.Now.AddDays(-4), TotalAmount = 2200, Status = "Completed", CreatedById = users["user3"] },
                    new Purchase { SupplierId = 5, PurchaseDate = DateTime.Now.AddDays(-2), TotalAmount = 1300, Status = "Pending", CreatedById = users["user4"] }
                };

                await context.Purchases.AddRangeAsync(purchases);
                await context.SaveChangesAsync(); // Ensure IDs are generated

                logger.LogInformation("✅ Purchases seeded.");

                // Now safely add Purchase Details using actual IDs
                if (!await context.PurchaseDetails.AnyAsync())
                {
                    var products = await context.Products.ToListAsync();

                    await context.PurchaseDetails.AddRangeAsync(
                        new PurchaseDetails { PurchaseId = purchases[0].PurchaseId, ProductId = products[0].ProductId, Quantity = 10, UnitCost = 100, subTotal = 1000 },
                        new PurchaseDetails { PurchaseId = purchases[1].PurchaseId, ProductId = products[1].ProductId, Quantity = 5, UnitCost = 150, subTotal = 750 },
                        new PurchaseDetails { PurchaseId = purchases[2].PurchaseId, ProductId = products[2].ProductId, Quantity = 8, UnitCost = 200, subTotal = 1600 },
                        new PurchaseDetails { PurchaseId = purchases[3].PurchaseId, ProductId = products[3].ProductId, Quantity = 6, UnitCost = 180, subTotal = 1080 },
                        new PurchaseDetails { PurchaseId = purchases[4].PurchaseId, ProductId = products[4].ProductId, Quantity = 3, UnitCost = 220, subTotal = 660 }
                    );
                    await context.SaveChangesAsync();
                    logger.LogInformation("✅ Purchase Details seeded.");
                }
            }


            // Sales
            if (!await context.Sales.AnyAsync())
            {
                var sales = new List<Sales>
                {
                    new Sales { CustomerId = 1, TotalAmount = 1500, SaleDate = DateTime.Now.AddDays(-10), Status = "Completed", CreatedBy = users["admin1"] },
                    new Sales { CustomerId = 2, TotalAmount = 800, SaleDate = DateTime.Now.AddDays(-5), Status = "Pending", CreatedBy = users["user1"] },
                    new Sales { CustomerId = 3, TotalAmount = 2000, SaleDate = DateTime.Now.AddDays(-3), Status = "Completed", CreatedBy = users["user2"] },
                    new Sales { CustomerId = 4, TotalAmount = 1200, SaleDate = DateTime.Now.AddDays(-2), Status = "Pending", CreatedBy = users["user3"] },
                    new Sales { CustomerId = 5, TotalAmount = 1800, SaleDate = DateTime.Now.AddDays(-1), Status = "Completed", CreatedBy = users["user4"] }
                };

                await context.Sales.AddRangeAsync(sales);
                await context.SaveChangesAsync();

                logger.LogInformation("✅ Sales seeded.");

                var products = await context.Products.ToListAsync();

                if (!await context.SaleDetails.AnyAsync())
                {
                    await context.SaleDetails.AddRangeAsync(
                        new SaleDetails { SaleId = sales[0].SaleId, ProductId = products[0].ProductId, Quantity = 3, UnitPrice = 200, Subtotal = 600 },
                        new SaleDetails { SaleId = sales[1].SaleId, ProductId = products[1].ProductId, Quantity = 2, UnitPrice = 300, Subtotal = 600 },
                        new SaleDetails { SaleId = sales[2].SaleId, ProductId = products[2].ProductId, Quantity = 5, UnitPrice = 250, Subtotal = 1250 },
                        new SaleDetails { SaleId = sales[3].SaleId, ProductId = products[3].ProductId, Quantity = 1, UnitPrice = 400, Subtotal = 400 },
                        new SaleDetails { SaleId = sales[4].SaleId, ProductId = products[4].ProductId, Quantity = 4, UnitPrice = 280, Subtotal = 1120 }
                    );
                    await context.SaveChangesAsync();
                    logger.LogInformation("✅ Sale Details seeded.");
                }
            }
        }
    }
}
