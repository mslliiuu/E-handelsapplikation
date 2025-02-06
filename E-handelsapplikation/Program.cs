using Microsoft.EntityFrameworkCore;
using E_handelsapplikation.Data;

var builder = WebApplication.CreateBuilder(args);

// Lägg till tjänster för MVC
builder.Services.AddControllersWithViews();

// Konfigurera DbContext för SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=E_handelsapplikation.db"));

builder.Services.AddHttpClient();


var app = builder.Build();

// Middleware för att hantera HTTP-förfrågningar
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Konfigurera standardrouten för MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}"); //På "controller" kan de även vara = Home, för att konfiguerar HomeController

app.Run();
