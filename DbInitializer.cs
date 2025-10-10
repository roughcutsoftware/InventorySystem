using InventorySystem.Core.Entities;
using InventorySystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure;

public static class DbInitializer
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDBContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await context.Database.MigrateAsync();
            await SeedDataAsync(context, userManager, roleManager);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }

    private static async Task SeedDataAsync(AppDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Seed Roles
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

        // Seed Users
        if (!context.Users.Any())
        {
            var users = new List<(ApplicationUser user, string password, string role)>
            {
                (new ApplicationUser
                {
                    Id = "U1",
                    UserName = "admin1@inventory.com",
                    Email = "admin1@inventory.com",
                    EmailConfirmed = true,
                    CreatedAt = new DateTime(2025, 1, 1)
                }, "Admin123!", "Admin"),
                
                (new ApplicationUser
                {
                    Id = "U2",
                    UserName = "admin2@inventory.com",
                    Email = "admin2@inventory.com",
                    EmailConfirmed = true,
                    CreatedAt = new DateTime(2025, 1, 1)
                }, "Admin123!", "Admin"),

                (new ApplicationUser
                {
                    Id = "U3",
                    UserName = "user1@inventory.com",
                    Email = "user1@inventory.com",
                    EmailConfirmed = true,
                    CreatedAt = new DateTime(2025, 1, 1)
                }, "User123!", "User"),

                (new ApplicationUser
                {
                    Id = "U4",
                    UserName = "user2@inventory.com",
                    Email = "user2@inventory.com",
                    EmailConfirmed = true,
                    CreatedAt = new DateTime(2025, 1, 1)
                }, "User123!", "User"),

                (new ApplicationUser
                {
                    Id = "U5",
                    UserName = "user3@inventory.com",
                    Email = "user3@inventory.com",
                    EmailConfirmed = true,
                    CreatedAt = new DateTime(2025, 1, 1)
                }, "User123!", "User")
            };

            foreach (var (user, password, role) in users)
            {
                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, role);
            }
        }

        // Seed Categories
        if (!context.Categories.Any())
        {
            var categories = new[]
            {
                new Category { CategoryId = 1, Name = "Electronics", Description = "Devices and gadgets" },
                new Category { CategoryId = 2, Name = "Clothing", Description = "Men and women clothes" },
                new Category { CategoryId = 3, Name = "Groceries", Description = "Food and beverages" },
                new Category { CategoryId = 4, Name = "Furniture", Description = "Home and office furniture" },
                new Category { CategoryId = 5, Name = "Sports", Description = "Sportswear and equipment" }
            };
            context.Categories.AddRange(categories);
        }

        // Seed Suppliers
        if (!context.Suppliers.Any())
        {
            var suppliers = new[]
            {
                new Supplier { SupplierId = 1, Name = "Ahmed Hassan", ContactName = "Ahmed", CompanyName = "Tech Supplies", Email = "ahmed@techsupplies.com", Phone = "01001234567", Address = "Cairo", IsActive = true },
                new Supplier { SupplierId = 2, Name = "Sara Ali", ContactName = "Sara", CompanyName = "Alpha Traders", Email = "sara@alphatraders.com", Phone = "01007654321", Address = "Giza", IsActive = true },
                new Supplier { SupplierId = 3, Name = "Mohamed Samir", ContactName = "Mohamed", CompanyName = "SmartParts", Email = "mohamed@smartparts.com", Phone = "01123456789", Address = "Alexandria", IsActive = true },
                new Supplier { SupplierId = 4, Name = "Nour Khaled", ContactName = "Nour", CompanyName = "ProLink", Email = "nour@prolink.com", Phone = "01009876543", Address = "Mansoura", IsActive = false },
                new Supplier { SupplierId = 5, Name = "Omar Fathy", ContactName = "Omar", CompanyName = "MegaTech", Email = "omar@megatech.com", Phone = "01234567890", Address = "Tanta", IsActive = true }
            };
            context.Suppliers.AddRange(suppliers);
        }

        // Seed Customers
        if (!context.Customers.Any())
        {
            var customers = new[]
            {
                new Customer { CustomerId = 1, Name = "Ahmed Ali", Email = "ahmed@example.com", Phone = "01011112222", Address = "Cairo, Egypt" },
                new Customer { CustomerId = 2, Name = "Sara Hassan", Email = "sara@example.com", Phone = "01033334444", Address = "Giza, Egypt" },
                new Customer { CustomerId = 3, Name = "Mona Salah", Email = "mona@example.com", Phone = "01055556666", Address = "Alexandria, Egypt" },
                new Customer { CustomerId = 4, Name = "Omar Fathy", Email = "omar@example.com", Phone = "01077778888", Address = "Tanta, Egypt" },
                new Customer { CustomerId = 5, Name = "Khaled Nasser", Email = "khaled@example.com", Phone = "01099990000", Address = "Aswan, Egypt" }
            };
            context.Customers.AddRange(customers);
        }

        // Seed Products
        if (!context.Products.Any())
        {
            var products = new[]
            {
                new Product { ProductId = 1, Name = "Smartphone", CategoryId = 1, SupplierId = 1, UnitPrice = 12000, CostPrice = 9000, QuantityInStock = 50, ReorderLevel = 10, IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
                new Product { ProductId = 2, Name = "Laptop", CategoryId = 1, SupplierId = 1, UnitPrice = 25000, CostPrice = 20000, QuantityInStock = 30, ReorderLevel = 5, IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
                new Product { ProductId = 3, Name = "T-shirt", CategoryId = 2, SupplierId = 2, UnitPrice = 300, CostPrice = 150, QuantityInStock = 100, ReorderLevel = 15, IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
                new Product { ProductId = 4, Name = "Apple Juice", CategoryId = 3, SupplierId = 3, UnitPrice = 50, CostPrice = 25, QuantityInStock = 200, ReorderLevel = 20, IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
                new Product { ProductId = 5, Name = "Chair", CategoryId = 4, SupplierId = 4, UnitPrice = 1000, CostPrice = 100, QuantityInStock = 15, ReorderLevel = 10, IsActive = true, CreatedAt = new DateTime(2025, 1, 1) }
            };
            context.Products.AddRange(products);
        }

        // Seed Sales and SaleDetails
        if (!context.Sales.Any())
        {
            var sales = new[]
            {
                new Sales { SaleId = 1, SaleDate = new DateTime(2025, 1, 1), TotalAmount = 1500.75m, Status = "Completed", CustomerId = 1, CreatedBy = "U1" },
                new Sales { SaleId = 2, SaleDate = new DateTime(2025, 1, 1), TotalAmount = 2200.50m, Status = "Pending", CustomerId = 2, CreatedBy = "U2" },
                new Sales { SaleId = 3, SaleDate = new DateTime(2025, 1, 1), TotalAmount = 890.00m, Status = "Completed", CustomerId = 3, CreatedBy = "U3" },
                new Sales { SaleId = 4, SaleDate = new DateTime(2025, 1, 1), TotalAmount = 4500.99m, Status = "Cancelled", CustomerId = 4, CreatedBy = "U3" },
                new Sales { SaleId = 5, SaleDate = new DateTime(2025, 1, 1), TotalAmount = 3200.10m, Status = "Completed", CustomerId = 5, CreatedBy = "U4" }
            };
            context.Sales.AddRange(sales);

            var saleDetails = new[]
            {
                new SaleDetails { SaleDetailId = 1, ProductId = 1, SaleId = 1, Quantity = 2, UnitPrice = 12000, Subtotal = 24000 },
                new SaleDetails { SaleDetailId = 2, ProductId = 2, SaleId = 2, Quantity = 1, UnitPrice = 25000, Subtotal = 25000 },
                new SaleDetails { SaleDetailId = 3, ProductId = 3, SaleId = 3, Quantity = 5, UnitPrice = 300, Subtotal = 1500 },
                new SaleDetails { SaleDetailId = 4, ProductId = 4, SaleId = 4, Quantity = 10, UnitPrice = 50, Subtotal = 500 },
                new SaleDetails { SaleDetailId = 5, ProductId = 5, SaleId = 5, Quantity = 1, UnitPrice = 1000, Subtotal = 1000 }
            };
            context.SaleDetails.AddRange(saleDetails);
        }

        // Seed Purchases and PurchaseDetails
        if (!context.Purchases.Any())
        {
            var purchases = new[]
            {
                new Purchase { PurchaseId = 1, PurchaseDate = new DateTime(2025, 1, 1), TotalAmount = 2500.00m, Status = "Completed", SupplierId = 1, CreatedById = "U1" },
                new Purchase { PurchaseId = 2, PurchaseDate = new DateTime(2025, 1, 1), TotalAmount = 1800.50m, Status = "Completed", SupplierId = 2, CreatedById = "U2" },
                new Purchase { PurchaseId = 3, PurchaseDate = new DateTime(2025, 1, 1), TotalAmount = 900.75m, Status = "Pending", SupplierId = 3, CreatedById = "U3" },
                new Purchase { PurchaseId = 4, PurchaseDate = new DateTime(2025, 1, 1), TotalAmount = 3100.00m, Status = "Completed", SupplierId = 1, CreatedById = "U4" },
                new Purchase { PurchaseId = 5, PurchaseDate = new DateTime(2025, 1, 1), TotalAmount = 1200.00m, Status = "Pending", SupplierId = 2, CreatedById = "U5" }
            };
            context.Purchases.AddRange(purchases);

            var purchaseDetails = new[]
            {
                new PurchaseDetails { PurchaseDetailId = 1, Quantity = 10, UnitCost = 100.50m, subTotal = 1005.00m, ProductId = 1, PurchaseId = 1 },
                new PurchaseDetails { PurchaseDetailId = 2, Quantity = 5, UnitCost = 200.00m, subTotal = 1000.00m, ProductId = 2, PurchaseId = 1 },
                new PurchaseDetails { PurchaseDetailId = 3, Quantity = 20, UnitCost = 50.25m, subTotal = 1005.00m, ProductId = 3, PurchaseId = 2 },
                new PurchaseDetails { PurchaseDetailId = 4, Quantity = 15, UnitCost = 75.00m, subTotal = 1125.00m, ProductId = 4, PurchaseId = 3 },
                new PurchaseDetails { PurchaseDetailId = 5, Quantity = 8, UnitCost = 120.75m, subTotal = 966.00m, ProductId = 5, PurchaseId = 4 }
            };
            context.PurchaseDetails.AddRange(purchaseDetails);
        }

        await context.SaveChangesAsync();
    }
}