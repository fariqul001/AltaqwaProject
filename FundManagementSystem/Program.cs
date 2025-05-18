
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FundManagementSystem.Models;


var builder = WebApplication.CreateBuilder(args);

// ✅ Get connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ✅ Add DbContext to the DI container (Dependency Injection)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// ✅ Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();  // For password reset, email confirmation, etc.

// ✅ Configure Cookie settings for Authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";  // Where to redirect when login is needed
    options.AccessDeniedPath = "/Account/AccessDenied";  // Where to redirect when access is denied
});

// ✅ Add MVC (Controllers + Views)
builder.Services.AddControllersWithViews();

// ✅ Add services for Razor Pages (if you're using them)
builder.Services.AddRazorPages();

var app = builder.Build();

// ✅ Middleware configuration
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Provides detailed error information in Development mode
}
else
{
    app.UseExceptionHandler("/Home/Error");  // Handles unhandled exceptions
    app.UseHsts();  // Ensures that browsers only use HTTPS for the app
}

app.UseHttpsRedirection();  // Enforces HTTPS on requests
app.UseStaticFiles();  // Serves static files (e.g., images, CSS, JS)

app.UseRouting();  // Enables routing

// ✅ Authentication and Authorization middlewares
app.UseAuthentication();  // Must be called before UseAuthorization
app.UseAuthorization();   // Ensures that authorization is checked before accessing protected routes

// ✅ Map MVC controllers to routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ✅ Map Razor Pages (if you're using them)
app.MapRazorPages();

app.Run();  // Run the app
