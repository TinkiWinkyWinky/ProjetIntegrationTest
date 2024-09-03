using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetIntegrationTest.Data;
using ProjetIntegrationTest.Models;
using ProjetIntegrationTest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    //options.EnableSensitiveDataLogging();
});

//builder.Services.AddScoped<VisitorService>();
//builder.Services.AddScoped<AdministratorService>();
builder.Services.AddScoped<AuctionService>();
builder.Services.AddScoped<LotService>();
builder.Services.AddScoped<SellerService>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

/*var passwordHasher = new PasswordHasher<ApplicationUser>();
var user = new ApplicationUser();
string passwordInClear = "Admin";
string hashedPassword = passwordHasher.HashPassword(user, passwordInClear);

Console.WriteLine("Hashed Password: " + hashedPassword);*/

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
