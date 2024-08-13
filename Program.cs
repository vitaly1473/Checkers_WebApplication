// файл Program.cs

using Microsoft.EntityFrameworkCore;
using CheckersClub.Data;
using Microsoft.AspNetCore.Authentication.Cookies; // добавили для хранения данных о пользователе

var builder = WebApplication.CreateBuilder(args);

//1
//  добавили для сессий
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Время жизни сессии
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//1

//2
// Добавляем сервисы аутентификации с использованием cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Путь к странице логина
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Время жизни cookie
        options.SlidingExpiration = true; // Пролонгация времени жизни cookie при активности пользователя
    });
//2

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// добавляем эту строку перед `app.UseRouting()`
app.UseSession();
app.UseRouting();

// Добавляем middleware для аутентификации и авторизации
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    //  pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
