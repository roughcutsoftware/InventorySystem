using InventorySystem.Core.Entities;
using InventorySystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InventorySystem.Infrastructure.Data;

public static class DbSeeder
{
    private static readonly DateTime _seedDate = new(2025, 1, 1);

    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDBContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var logger = services.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("Starting database initialization...");

            await context.Database.MigrateAsync();
            await SeedDataAsync(context, userManager, roleManager, logger);

            logger.LogInformation("Database initialization completed successfully.");
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw; // Rethrow to ensure the app doesn't start with an unseeded database
        }
    }

    private static async Task SeedDataAsync(
        AppDBContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger logger)
    {
        await SeedRolesAsync(roleManager, logger);
        await SeedUsersAsync(userManager, logger);
        await SeedCategoriesAsync(context, logger);
        await SeedSuppliersAsync(context, logger);
        await SeedCustomersAsync(context, logger);
        await SeedProductsAsync(context, logger);
        await SeedSalesAsync(context, logger);
        await SeedPurchasesAsync(context, logger);

        logger.LogInformation("All seed operations completed successfully.");
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, ILogger logger)
    {
        logger.LogInformation("Seeding roles...");

        var roles = new[] { "Admin", "User" };
        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole(roleName)
                {
                    NormalizedName = roleName.ToUpper()
                };
                await roleManager.CreateAsync(role);
                logger.LogInformation("Created role: {RoleName}", roleName);
            }
        }
    }

    private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, ILogger logger)
    {
        logger.LogInformation("Seeding users...");

        var users = new[]
        {
            (User: new ApplicationUser
            {
                Id = "U1",
                UserName = "admin1@inventory.com",
                Email = "admin1@inventory.com",
                EmailConfirmed = true,
                CreatedAt = _seedDate
            }, Password: "Admin123!", Role: "Admin"),

            (User: new ApplicationUser
            {
                Id = "U2",
                UserName = "admin2@inventory.com",
                Email = "admin2@inventory.com",
                EmailConfirmed = true,
                CreatedAt = _seedDate
            }, Password: "Admin123!", Role: "Admin"),

            (User: new ApplicationUser
            {
                Id = "U3",
                UserName = "user1@inventory.com",
                Email = "user1@inventory.com",
                EmailConfirmed = true,
                CreatedAt = _seedDate
            }, Password: "User123!", Role: "User"),

            (User: new ApplicationUser
            {
                Id = "U4",
                UserName = "user2@inventory.com",
                Email = "user2@inventory.com",
                EmailConfirmed = true,
                CreatedAt = _seedDate
            }, Password: "User123!", Role: "User"),

            (User: new ApplicationUser
            {
                Id = "U5",
                UserName = "user3@inventory.com",
                Email = "user3@inventory.com",
                EmailConfirmed = true,
                CreatedAt = _seedDate
            }, Password: "User123!", Role: "User")
        };

        foreach (var (user, password, role) in users)
        {
            if (await userManager.FindByEmailAsync(user.Email) == null)
            {
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                    logger.LogInformation("Created user: {UserEmail} with role: {Role}", user.Email, role);
                }
                else
                {
                    logger.LogError("Failed to create user: {UserEmail}. Errors: {Errors}",
                        user.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }

    private static async Task SeedCategoriesAsync(AppDBContext context, ILogger logger)
    {
        if (await context.Categories.AnyAsync())
            return;

        logger.LogInformation("Seeding categories...");

        var categories = new[]
        {
            new Category { CategoryId = 1, Name = "Electronics", Description = "Devices and gadgets" },
            new Category { CategoryId = 2, Name = "Clothing", Description = "Men and women clothes" },
            new Category { CategoryId = 3, Name = "Groceries", Description = "Food and beverages" },
            new Category { CategoryId = 4, Name = "Furniture", Description = "Home and office furniture" },
            new Category { CategoryId = 5, Name = "Sports", Description = "Sportswear and equipment" }
        };

        context.Categories.AddRange(categories);
        await context.SaveChangesAsync();
        logger.LogInformation("Added {Count} categories", categories.Length);
    }

    private static async Task SeedSuppliersAsync(AppDBContext context, ILogger logger)
    {
        if (await context.Suppliers.AnyAsync())
            return;

        logger.LogInformation("Seeding suppliers...");

        var suppliers = new[]
        {
            new Supplier { SupplierId = 1, Name = "Ahmed Hassan", ContactName = "Ahmed", CompanyName = "Tech Supplies", Email = "ahmed@techsupplies.com", Phone = "01001234567", Address = "Cairo", IsActive = true },
            new Supplier { SupplierId = 2, Name = "Sara Ali", ContactName = "Sara", CompanyName = "Alpha Traders", Email = "sara@alphatraders.com", Phone = "01007654321", Address = "Giza", IsActive = true },
            new Supplier { SupplierId = 3, Name = "Mohamed Samir", ContactName = "Mohamed", CompanyName = "SmartParts", Email = "mohamed@smartparts.com", Phone = "01123456789", Address = "Alexandria", IsActive = true },
            new Supplier { SupplierId = 4, Name = "Nour Khaled", ContactName = "Nour", CompanyName = "ProLink", Email = "nour@prolink.com", Phone = "01009876543", Address = "Mansoura", IsActive = false },
            new Supplier { SupplierId = 5, Name = "Omar Fathy", ContactName = "Omar", CompanyName = "MegaTech", Email = "omar@megatech.com", Phone = "01234567890", Address = "Tanta", IsActive = true }
        };

        context.Suppliers.AddRange(suppliers);
        await context.SaveChangesAsync();
        logger.LogInformation("Added {Count} suppliers", suppliers.Length);
    }

    private static async Task SeedCustomersAsync(AppDBContext context, ILogger logger)
    {
        if (await context.Customers.AnyAsync())
            return;

        logger.LogInformation("Seeding customers...");

        var customers = new[]
        {
            new Customer { CustomerId = 1, Name = "Ahmed Ali", Email = "ahmed@example.com", Phone = "01011112222", Address = "Cairo, Egypt" },
            new Customer { CustomerId = 2, Name = "Sara Hassan", Email = "sara@example.com", Phone = "01033334444", Address = "Giza, Egypt" },
            new Customer { CustomerId = 3, Name = "Mona Salah", Email = "mona@example.com", Phone = "01055556666", Address = "Alexandria, Egypt" },
            new Customer { CustomerId = 4, Name = "Omar Fathy", Email = "omar@example.com", Phone = "01077778888", Address = "Tanta, Egypt" },
            new Customer { CustomerId = 5, Name = "Khaled Nasser", Email = "khaled@example.com", Phone = "01099990000", Address = "Aswan, Egypt" }
        };

        context.Customers.AddRange(customers);
        await context.SaveChangesAsync();
        logger.LogInformation("Added {Count} customers", customers.Length);
    }

    private static async Task SeedProductsAsync(AppDBContext context, ILogger logger)
    {
        if (await context.Products.AnyAsync())
            return;

        logger.LogInformation("Seeding products...");

        var products = new[]
        {
            new Product { ProductId = 1, Name = "Smartphone", CategoryId = 1, SupplierId = 1, UnitPrice = 12000, CostPrice = 9000, QuantityInStock = 50, ReorderLevel = 10, IsActive = true, CreatedAt = _seedDate },
            new Product { ProductId = 2, Name = "Laptop", CategoryId = 1, SupplierId = 1, UnitPrice = 25000, CostPrice = 20000, QuantityInStock = 30, ReorderLevel = 5, IsActive = true, CreatedAt = _seedDate },
            new Product { ProductId = 3, Name = "T-shirt", CategoryId = 2, SupplierId = 2, UnitPrice = 300, CostPrice = 150, QuantityInStock = 100, ReorderLevel = 15, IsActive = true, CreatedAt = _seedDate },
            new Product { ProductId = 4, Name = "Apple Juice", CategoryId = 3, SupplierId = 3, UnitPrice = 50, CostPrice = 25, QuantityInStock = 200, ReorderLevel = 20, IsActive = true, CreatedAt = _seedDate },
            new Product { ProductId = 5, Name = "Chair", CategoryId = 4, SupplierId = 4, UnitPrice = 1000, CostPrice = 100, QuantityInStock = 15, ReorderLevel = 10, IsActive = true, CreatedAt = _seedDate }
        };

        context.Products.AddRange(products);
        await context.SaveChangesAsync();
        logger.LogInformation("Added {Count} products", products.Length);
    }

    private static async Task SeedSalesAsync(AppDBContext context, ILogger logger)
    {
        if (await context.Sales.AnyAsync())
            return;

        logger.LogInformation("Seeding sales and sale details...");

        var sales = new[]
        {
            new Sales { SaleId = 1, SaleDate = _seedDate, TotalAmount = 1500.75m, Status = "Completed", CustomerId = 1, CreatedBy = "U1" },
            new Sales { SaleId = 2, SaleDate = _seedDate, TotalAmount = 2200.50m, Status = "Pending", CustomerId = 2, CreatedBy = "U2" },
            new Sales { SaleId = 3, SaleDate = _seedDate, TotalAmount = 890.00m, Status = "Completed", CustomerId = 3, CreatedBy = "U3" },
            new Sales { SaleId = 4, SaleDate = _seedDate, TotalAmount = 4500.99m, Status = "Cancelled", CustomerId = 4, CreatedBy = "U3" },
            new Sales { SaleId = 5, SaleDate = _seedDate, TotalAmount = 3200.10m, Status = "Completed", CustomerId = 5, CreatedBy = "U4" }
        };

        var saleDetails = new[]
        {
            new SaleDetails { SaleDetailId = 1, ProductId = 1, SaleId = 1, Quantity = 2, UnitPrice = 12000, Subtotal = 24000 },
            new SaleDetails { SaleDetailId = 2, ProductId = 2, SaleId = 2, Quantity = 1, UnitPrice = 25000, Subtotal = 25000 },
            new SaleDetails { SaleDetailId = 3, ProductId = 3, SaleId = 3, Quantity = 5, UnitPrice = 300, Subtotal = 1500 },
            new SaleDetails { SaleDetailId = 4, ProductId = 4, SaleId = 4, Quantity = 10, UnitPrice = 50, Subtotal = 500 },
            new SaleDetails { SaleDetailId = 5, ProductId = 5, SaleId = 5, Quantity = 1, UnitPrice = 1000, Subtotal = 1000 }
        };

        context.Sales.AddRange(sales);
        context.SaleDetails.AddRange(saleDetails);
        await context.SaveChangesAsync();
        logger.LogInformation("Added {SalesCount} sales and {DetailsCount} sale details", sales.Length, saleDetails.Length);
    }

    private static async Task SeedPurchasesAsync(AppDBContext context, ILogger logger)
    {
        if (await context.Purchases.AnyAsync())
            return;

        logger.LogInformation("Seeding purchases and purchase details...");

        var purchases = new[]
        {
            new Purchase { PurchaseId = 1, PurchaseDate = _seedDate, TotalAmount = 2500.00m, Status = "Completed", SupplierId = 1, CreatedById = "U1" },
            new Purchase { PurchaseId = 2, PurchaseDate = _seedDate, TotalAmount = 1800.50m, Status = "Completed", SupplierId = 2, CreatedById = "U2" },
            new Purchase { PurchaseId = 3, PurchaseDate = _seedDate, TotalAmount = 900.75m, Status = "Pending", SupplierId = 3, CreatedById = "U3" },
            new Purchase { PurchaseId = 4, PurchaseDate = _seedDate, TotalAmount = 3100.00m, Status = "Completed", SupplierId = 1, CreatedById = "U4" },
            new Purchase { PurchaseId = 5, PurchaseDate = _seedDate, TotalAmount = 1200.00m, Status = "Pending", SupplierId = 2, CreatedById = "U5" }
        };

        var purchaseDetails = new[]
        {
            new PurchaseDetails { PurchaseDetailId = 1, Quantity = 10, UnitCost = 100.50m, subTotal = 1005.00m, ProductId = 1, PurchaseId = 1 },
            new PurchaseDetails { PurchaseDetailId = 2, Quantity = 5, UnitCost = 200.00m, subTotal = 1000.00m, ProductId = 2, PurchaseId = 1 },
            new PurchaseDetails { PurchaseDetailId = 3, Quantity = 20, UnitCost = 50.25m, subTotal = 1005.00m, ProductId = 3, PurchaseId = 2 },
            new PurchaseDetails { PurchaseDetailId = 4, Quantity = 15, UnitCost = 75.00m, subTotal = 1125.00m, ProductId = 4, PurchaseId = 3 },
            new PurchaseDetails { PurchaseDetailId = 5, Quantity = 8, UnitCost = 120.75m, subTotal = 966.00m, ProductId = 5, PurchaseId = 4 }
        };

        context.Purchases.AddRange(purchases);
        context.PurchaseDetails.AddRange(purchaseDetails);
        await context.SaveChangesAsync();
        logger.LogInformation("Added {PurchasesCount} purchases and {DetailsCount} purchase details", purchases.Length, purchaseDetails.Length);
    }
}