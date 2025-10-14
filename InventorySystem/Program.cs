using InventorySystem.Core.Entities;
using InventorySystem.Core.Hubs;
using InventorySystem.Core.Interfaces;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Core.Interfaces.Services;
using InventorySystem.Infrastructure.Data;
using InventorySystem.Infrastructure.Repositories;
using InventorySystem.Infrastructure.Services;
using InventorySystem.web.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<AppDBContext>()
.AddDefaultTokenProviders();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddSignalR();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ISalesRepository, SalesRepository>();


builder.Services.AddAutoMapper(op=>op.AddProfile(typeof(AutoMapperProfile)));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapHub<NotificationHub>("/notificationHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}")
    .WithStaticAssets();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var context = services.GetRequiredService<AppDBContext>();
//    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
//    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
//    var logger = services.GetRequiredService<ILogger<Program>>();
//    await DbSeeder.InitializeAsync(context, userManager, roleManager, logger);
//}


app.Run();
