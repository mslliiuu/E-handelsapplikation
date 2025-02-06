using Microsoft.EntityFrameworkCore;
using E_handelsapplikation.Data;

var builder = WebApplication.CreateBuilder(args);

// L�gg till tj�nster f�r MVC
builder.Services.AddControllersWithViews();

// Konfigurera DbContext f�r SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=E_handelsapplikation.db"));

builder.Services.AddHttpClient();


var app = builder.Build();

// Middleware f�r att hantera HTTP-f�rfr�gningar
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Konfigurera standardrouten f�r MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}"); //P� "controller" kan de �ven vara = Home, f�r att konfiguerar HomeController

app.Run();
