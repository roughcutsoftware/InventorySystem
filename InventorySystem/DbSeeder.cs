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
                    new Supplier { Name = "TechVision", ContactName = "Omar Hassan", CompanyName = "TechVision Solutions", Email = "contact@techvision.com", Phone = "0101001001", Address = "Cairo", IsActive = true },
                    new Supplier { Name = "SafeZone", ContactName = "Sara Adel", CompanyName = "SafeZone Security", Email = "info@safezone.com", Phone = "0101001002", Address = "Alexandria", IsActive = true },
                    new Supplier { Name = "NetLink", ContactName = "Mohamed Fathy", CompanyName = "NetLink Communications", Email = "sales@netlink.com", Phone = "0101001003", Address = "Giza", IsActive = true },
                    new Supplier { Name = "CamPro", ContactName = "Lina Mostafa", CompanyName = "CamPro Systems", Email = "support@campro.com", Phone = "0101001004", Address = "Mansoura", IsActive = true },
                    new Supplier { Name = "SecureMax", ContactName = "Ahmed Samir", CompanyName = "SecureMax Technologies", Email = "info@securemax.com", Phone = "0101001005", Address = "Tanta", IsActive = true },
                    new Supplier { Name = "BrightCom", ContactName = "Nour El-Din", CompanyName = "BrightCom Networks", Email = "info@brightcom.com", Phone = "0101001006", Address = "Cairo", IsActive = true },
                    new Supplier { Name = "VisionTech", ContactName = "Heba Nasser", CompanyName = "VisionTech Solutions", Email = "sales@visiontech.com", Phone = "0101001007", Address = "Alexandria", IsActive = true },
                    new Supplier { Name = "AlphaLink", ContactName = "Karim Said", CompanyName = "AlphaLink Communications", Email = "contact@alphalink.com", Phone = "0101001008", Address = "Giza", IsActive = true },
                    new Supplier { Name = "NextGuard", ContactName = "Rana Fouad", CompanyName = "NextGuard Security", Email = "info@nextguard.com", Phone = "0101001009", Address = "Suez", IsActive = true },
                    new Supplier { Name = "DataWave", ContactName = "Tamer Khalil", CompanyName = "DataWave Systems", Email = "support@datawave.com", Phone = "0101001010", Address = "Port Said", IsActive = true },
                    new Supplier { Name = "SmartEdge", ContactName = "Mona Ibrahim", CompanyName = "SmartEdge Solutions", Email = "hello@smartedge.com", Phone = "0101001011", Address = "Cairo", IsActive = true },
                    new Supplier { Name = "CyberLine", ContactName = "Youssef Amin", CompanyName = "CyberLine Technologies", Email = "sales@cyberline.com", Phone = "0101001012", Address = "Fayoum", IsActive = true },
                    new Supplier { Name = "WaveNet", ContactName = "Dina Mahmoud", CompanyName = "WaveNet Communications", Email = "info@wavenet.com", Phone = "0101001013", Address = "Aswan", IsActive = true },
                    new Supplier { Name = "GuardPlus", ContactName = "Mahmoud Tarek", CompanyName = "GuardPlus Security Systems", Email = "support@guardplus.com", Phone = "0101001014", Address = "Luxor", IsActive = true },
                    new Supplier { Name = "InfoGate", ContactName = "Laila Hassan", CompanyName = "InfoGate Technologies", Email = "contact@infogate.com", Phone = "0101001015", Address = "Ismailia", IsActive = true }
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
                    new Customer { Name = "Hassan Nabil", Email = "hassan@example.com", Phone = "0101000005", Address = "Tanta" },
                    new Customer { Name = "Laila Mostafa", Email = "laila@example.com", Phone = "0101000006", Address = "Cairo" },
                    new Customer { Name = "Youssef Adel", Email = "youssef@example.com", Phone = "0101000007", Address = "Ismailia" },
                    new Customer { Name = "Dina Mohamed", Email = "dina@example.com", Phone = "0101000008", Address = "Fayoum" },
                    new Customer { Name = "Karim Samir", Email = "karim@example.com", Phone = "0101000009", Address = "Aswan" },
                    new Customer { Name = "Nour Hany", Email = "nour@example.com", Phone = "0101000010", Address = "Luxor" },
                    new Customer { Name = "Tamer Khalil", Email = "tamer@example.com", Phone = "0101000011", Address = "Port Said" },
                    new Customer { Name = "Reem Farouk", Email = "reem@example.com", Phone = "0101000012", Address = "Suez" },
                    new Customer { Name = "Mahmoud Ehab", Email = "mahmoud@example.com", Phone = "0101000013", Address = "Zagazig" },
                    new Customer { Name = "Nadia Fathy", Email = "nadia@example.com", Phone = "0101000014", Address = "Cairo" },
                    new Customer { Name = "Ahmed Lotfy", Email = "ahmed@example.com", Phone = "0101000015", Address = "Giza" },
                    new Customer { Name = "Salma Ibrahim", Email = "salma@example.com", Phone = "0101000016", Address = "Alexandria" },
                    new Customer { Name = "Hany Saber", Email = "hany@example.com", Phone = "0101000017", Address = "Tanta" },
                    new Customer { Name = "Rania Khaled", Email = "rania@example.com", Phone = "0101000018", Address = "Cairo" },
                    new Customer { Name = "Eman Hussein", Email = "eman@example.com", Phone = "0101000019", Address = "Mansoura" },
                    new Customer { Name = "Mostafa Shawky", Email = "mostafa@example.com", Phone = "0101000020", Address = "Giza" }
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
                    new Category { Name = "Accessories", Description = "Cables, mounts, and power adapters" },
                    new Category { Name = "Networking", Description = "Routers, switches, and access points" },
                    new Category { Name = "Storage", Description = "Hard drives, SSDs, and storage devices" },
                    new Category { Name = "Monitors", Description = "Display monitors for surveillance systems" },
                    new Category { Name = "Power Supplies", Description = "Power adapters, UPS, and PoE injectors" },
                    new Category { Name = "Smart Home", Description = "Smart locks, sensors, and home automation devices" },
                    new Category { Name = "Software", Description = "Surveillance and management software" },
                    new Category { Name = "Cabling", Description = "Ethernet cables, connectors, and patch panels" },
                    new Category { Name = "Intercom Systems", Description = "Audio and video intercom devices" },
                    new Category { Name = "Alarm Systems", Description = "Burglar alarms, motion detectors, and sirens" },
                    new Category { Name = "Access Control", Description = "Biometric readers, RFID systems, and keypads" },
                    new Category { Name = "Mounting Equipment", Description = "Brackets, stands, and enclosures" },
                    new Category { Name = "Lighting", Description = "Infrared lights, floodlights, and smart lighting solutions" }
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
                    new Product { Name = "WD Purple 2TB", QuantityInStock = 20, UnitPrice = 900, CostPrice = 700, ReorderLevel = 5, CreatedAt = DateTime.Now, CategoryId = cats[4].CategoryId, SupplierId = sups[0].SupplierId },
                    new Product { Name = "IP Camera 4K", QuantityInStock = 18, UnitPrice = 2800, CostPrice = 2300, ReorderLevel = 4, CreatedAt = DateTime.Now, CategoryId = cats[0].CategoryId, SupplierId = sups[0].SupplierId },
                    new Product { Name = "Dome Camera 5MP", QuantityInStock = 30, UnitPrice = 1600, CostPrice = 1300, ReorderLevel = 6, CreatedAt = DateTime.Now, CategoryId = cats[0].CategoryId, SupplierId = sups[3].SupplierId },
                    new Product { Name = "PTZ Camera Outdoor", QuantityInStock = 12, UnitPrice = 4200, CostPrice = 3600, ReorderLevel = 2, CreatedAt = DateTime.Now, CategoryId = cats[0].CategoryId, SupplierId = sups[8].SupplierId },
                    new Product { Name = "DVR 16 Channel", QuantityInStock = 8, UnitPrice = 4200, CostPrice = 3500, ReorderLevel = 2, CreatedAt = DateTime.Now, CategoryId = cats[1].CategoryId, SupplierId = sups[4].SupplierId },
                    new Product { Name = "NVR 16 Channel", QuantityInStock = 6, UnitPrice = 4800, CostPrice = 4000, ReorderLevel = 2, CreatedAt = DateTime.Now, CategoryId = cats[1].CategoryId, SupplierId = sups[7].SupplierId },
                    new Product { Name = "Ethernet Cable Cat6 50m", QuantityInStock = 50, UnitPrice = 500, CostPrice = 350, ReorderLevel = 10, CreatedAt = DateTime.Now, CategoryId = cats[2].CategoryId, SupplierId = sups[2].SupplierId },
                    new Product { Name = "Wall Mount Bracket", QuantityInStock = 80, UnitPrice = 100, CostPrice = 60, ReorderLevel = 15, CreatedAt = DateTime.Now, CategoryId = cats[2].CategoryId, SupplierId = sups[5].SupplierId },
                    new Product { Name = "Network Switch 8-Port Gigabit", QuantityInStock = 14, UnitPrice = 950, CostPrice = 700, ReorderLevel = 4, CreatedAt = DateTime.Now, CategoryId = cats[3].CategoryId, SupplierId = sups[2].SupplierId },
                    new Product { Name = "PoE Switch 16-Port", QuantityInStock = 10, UnitPrice = 2200, CostPrice = 1800, ReorderLevel = 3, CreatedAt = DateTime.Now, CategoryId = cats[3].CategoryId, SupplierId = sups[6].SupplierId },
                    new Product { Name = "Seagate SkyHawk 4TB", QuantityInStock = 12, UnitPrice = 1500, CostPrice = 1200, ReorderLevel = 4, CreatedAt = DateTime.Now, CategoryId = cats[4].CategoryId, SupplierId = sups[0].SupplierId },
                    new Product { Name = "SSD 1TB", QuantityInStock = 25, UnitPrice = 2200, CostPrice = 1800, ReorderLevel = 5, CreatedAt = DateTime.Now, CategoryId = cats[4].CategoryId, SupplierId = sups[10].SupplierId },
                    new Product { Name = "LED Monitor 22\"", QuantityInStock = 10, UnitPrice = 2300, CostPrice = 1900, ReorderLevel = 2, CreatedAt = DateTime.Now, CategoryId = cats[5].CategoryId, SupplierId = sups[1].SupplierId },
                    new Product { Name = "LED Monitor 27\"", QuantityInStock = 7, UnitPrice = 3100, CostPrice = 2600, ReorderLevel = 2, CreatedAt = DateTime.Now, CategoryId = cats[5].CategoryId, SupplierId = sups[1].SupplierId },
                    new Product { Name = "UPS 1000VA", QuantityInStock = 9, UnitPrice = 1800, CostPrice = 1500, ReorderLevel = 3, CreatedAt = DateTime.Now, CategoryId = cats[6].CategoryId, SupplierId = sups[9].SupplierId },
                    new Product { Name = "PoE Injector", QuantityInStock = 20, UnitPrice = 300, CostPrice = 220, ReorderLevel = 5, CreatedAt = DateTime.Now, CategoryId = cats[6].CategoryId, SupplierId = sups[9].SupplierId },
                    new Product { Name = "Smart Door Lock", QuantityInStock = 10, UnitPrice = 2600, CostPrice = 2100, ReorderLevel = 2, CreatedAt = DateTime.Now, CategoryId = cats[7].CategoryId, SupplierId = sups[8].SupplierId },
                    new Product { Name = "Motion Sensor", QuantityInStock = 30, UnitPrice = 400, CostPrice = 250, ReorderLevel = 5, CreatedAt = DateTime.Now, CategoryId = cats[7].CategoryId, SupplierId = sups[8].SupplierId },
                    new Product { Name = "Surveillance Software Pro", QuantityInStock = 50, UnitPrice = 500, CostPrice = 300, ReorderLevel = 10, CreatedAt = DateTime.Now, CategoryId = cats[8].CategoryId, SupplierId = sups[0].SupplierId },
                    new Product { Name = "RJ45 Connector Pack (100 pcs)", QuantityInStock = 200, UnitPrice = 250, CostPrice = 150, ReorderLevel = 20, CreatedAt = DateTime.Now, CategoryId = cats[9].CategoryId, SupplierId = sups[5].SupplierId },
                    new Product { Name = "Video Intercom Set", QuantityInStock = 5, UnitPrice = 5500, CostPrice = 4700, ReorderLevel = 1, CreatedAt = DateTime.Now, CategoryId = cats[10].CategoryId, SupplierId = sups[7].SupplierId },
                    new Product { Name = "Alarm Siren Outdoor", QuantityInStock = 25, UnitPrice = 700, CostPrice = 500, ReorderLevel = 5, CreatedAt = DateTime.Now, CategoryId = cats[11].CategoryId, SupplierId = sups[4].SupplierId },
                    new Product { Name = "Fingerprint Reader", QuantityInStock = 15, UnitPrice = 1900, CostPrice = 1500, ReorderLevel = 3, CreatedAt = DateTime.Now, CategoryId = cats[12].CategoryId, SupplierId = sups[9].SupplierId },
                    new Product { Name = "Metal Camera Mount", QuantityInStock = 60, UnitPrice = 120, CostPrice = 80, ReorderLevel = 10, CreatedAt = DateTime.Now, CategoryId = cats[13].CategoryId, SupplierId = sups[5].SupplierId },
                    new Product { Name = "Infrared Floodlight", QuantityInStock = 10, UnitPrice = 800, CostPrice = 600, ReorderLevel = 3, CreatedAt = DateTime.Now, CategoryId = cats[14].CategoryId, SupplierId = sups[6].SupplierId }
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
                    new Purchase { SupplierId = 5, PurchaseDate = DateTime.Now.AddDays(-2), TotalAmount = 1300, Status = "Pending", CreatedById = users["user4"] },
                    new Purchase { SupplierId = 6, PurchaseDate = DateTime.Now.AddDays(-15), TotalAmount = 3200, Status = "Completed", CreatedById = users["admin1"] },
                    new Purchase { SupplierId = 7, PurchaseDate = DateTime.Now.AddDays(-9), TotalAmount = 4100, Status = "Completed", CreatedById = users["user2"] },
                    new Purchase { SupplierId = 8, PurchaseDate = DateTime.Now.AddDays(-5), TotalAmount = 2800, Status = "Completed", CreatedById = users["user3"] },
                    new Purchase { SupplierId = 9, PurchaseDate = DateTime.Now.AddDays(-3), TotalAmount = 3600, Status = "Pending", CreatedById = users["user1"] },
                    new Purchase { SupplierId = 10, PurchaseDate = DateTime.Now.AddDays(-1), TotalAmount = 2400, Status = "Completed", CreatedById = users["admin1"] }
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
                    new PurchaseDetails { PurchaseId = purchases[4].PurchaseId, ProductId = products[4].ProductId, Quantity = 3, UnitCost = 220, subTotal = 660 },
                    new PurchaseDetails { PurchaseId = purchases[5].PurchaseId, ProductId = products[5].ProductId, Quantity = 5, UnitCost = 400, subTotal = 2000 },
                    new PurchaseDetails { PurchaseId = purchases[5].PurchaseId, ProductId = products[6].ProductId, Quantity = 3, UnitCost = 400, subTotal = 1200 },
                    new PurchaseDetails { PurchaseId = purchases[6].PurchaseId, ProductId = products[8].ProductId, Quantity = 4, UnitCost = 900, subTotal = 3600 },
                    new PurchaseDetails { PurchaseId = purchases[6].PurchaseId, ProductId = products[2].ProductId, Quantity = 2, UnitCost = 250, subTotal = 500 },
                    new PurchaseDetails { PurchaseId = purchases[7].PurchaseId, ProductId = products[10].ProductId, Quantity = 8, UnitCost = 300, subTotal = 2400 },
                    new PurchaseDetails { PurchaseId = purchases[7].PurchaseId, ProductId = products[11].ProductId, Quantity = 4, UnitCost = 500, subTotal = 2000 },
                    new PurchaseDetails { PurchaseId = purchases[8].PurchaseId, ProductId = products[15].ProductId, Quantity = 6, UnitCost = 500, subTotal = 3000 },
                    new PurchaseDetails { PurchaseId = purchases[8].PurchaseId, ProductId = products[17].ProductId, Quantity = 2, UnitCost = 300, subTotal = 600 },
                    new PurchaseDetails { PurchaseId = purchases[9].PurchaseId, ProductId = products[13].ProductId, Quantity = 3, UnitCost = 800, subTotal = 2400 }
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
                    new Sales { CustomerId = 5, TotalAmount = 1800, SaleDate = DateTime.Now.AddDays(-1), Status = "Completed", CreatedBy = users["user4"] },
                    new Sales { CustomerId = 6, TotalAmount = 950, SaleDate = DateTime.Now.AddDays(-9), Status = "Completed", CreatedBy = users["user1"] },
                    new Sales { CustomerId = 7, TotalAmount = 2500, SaleDate = DateTime.Now.AddDays(-8), Status = "Completed", CreatedBy = users["user2"] },
                    new Sales { CustomerId = 8, TotalAmount = 1300, SaleDate = DateTime.Now.AddDays(-6), Status = "Pending", CreatedBy = users["user3"] },
                    new Sales { CustomerId = 9, TotalAmount = 2200, SaleDate = DateTime.Now.AddDays(-4), Status = "Completed", CreatedBy = users["admin1"] },
                    new Sales { CustomerId = 10, TotalAmount = 700, SaleDate = DateTime.Now.AddDays(-2), Status = "Cancelled", CreatedBy = users["user4"] },
                    new Sales { CustomerId = 11, TotalAmount = 3100, SaleDate = DateTime.Now.AddDays(-1), Status = "Completed", CreatedBy = users["user2"] },
                    new Sales { CustomerId = 12, TotalAmount = 1900, SaleDate = DateTime.Now, Status = "Pending", CreatedBy = users["user3"] },
                    new Sales { CustomerId = 13, TotalAmount = 2600, SaleDate = DateTime.Now, Status = "Completed", CreatedBy = users["admin1"] },
                    new Sales { CustomerId = 14, TotalAmount = 1500, SaleDate = DateTime.Now, Status = "Pending", CreatedBy = users["user4"] },
                    new Sales { CustomerId = 15, TotalAmount = 2300, SaleDate = DateTime.Now, Status = "Completed", CreatedBy = users["user1"] }
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
                    new SaleDetails { SaleId = sales[4].SaleId, ProductId = products[4].ProductId, Quantity = 4, UnitPrice = 280, Subtotal = 1120 },
                    new SaleDetails { SaleId = sales[5].SaleId, ProductId = products[5].ProductId, Quantity = 2, UnitPrice = 450, Subtotal = 900 },
                    new SaleDetails { SaleId = sales[6].SaleId, ProductId = products[6].ProductId, Quantity = 4, UnitPrice = 600, Subtotal = 2400 },
                    new SaleDetails { SaleId = sales[6].SaleId, ProductId = products[2].ProductId, Quantity = 1, UnitPrice = 100, Subtotal = 100 },
                    new SaleDetails { SaleId = sales[7].SaleId, ProductId = products[9].ProductId, Quantity = 3, UnitPrice = 400, Subtotal = 1200 },
                    new SaleDetails { SaleId = sales[8].SaleId, ProductId = products[10].ProductId, Quantity = 2, UnitPrice = 1000, Subtotal = 2000 },
                    new SaleDetails { SaleId = sales[9].SaleId, ProductId = products[2].ProductId, Quantity = 2, UnitPrice = 350, Subtotal = 700 },
                    new SaleDetails { SaleId = sales[10].SaleId, ProductId = products[11].ProductId, Quantity = 4, UnitPrice = 750, Subtotal = 3000 },
                    new SaleDetails { SaleId = sales[10].SaleId, ProductId = products[3].ProductId, Quantity = 1, UnitPrice = 100, Subtotal = 100 },
                    new SaleDetails { SaleId = sales[11].SaleId, ProductId = products[12].ProductId, Quantity = 3, UnitPrice = 600, Subtotal = 1800 },
                    new SaleDetails { SaleId = sales[11].SaleId, ProductId = products[14].ProductId, Quantity = 1, UnitPrice = 100, Subtotal = 100 },
                    new SaleDetails { SaleId = sales[12].SaleId, ProductId = products[15].ProductId, Quantity = 5, UnitPrice = 500, Subtotal = 2500 },
                    new SaleDetails { SaleId = sales[13].SaleId, ProductId = products[16].ProductId, Quantity = 3, UnitPrice = 500, Subtotal = 1500 },
                    new SaleDetails { SaleId = sales[14].SaleId, ProductId = products[17].ProductId, Quantity = 2, UnitPrice = 1150, Subtotal = 2300 }
                    );
                    await context.SaveChangesAsync();
                    logger.LogInformation("✅ Sale Details seeded.");
                }
            }
        }
    }
}
