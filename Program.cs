// ���� Program.cs

using Microsoft.EntityFrameworkCore;
using CheckersClub.Data;
using Microsoft.AspNetCore.Authentication.Cookies; // �������� ��� �������� ������ � ������������

var builder = WebApplication.CreateBuilder(args);

//1
//  �������� ��� ������
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // ����� ����� ������
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//1

//2
// ��������� ������� �������������� � �������������� cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // ���� � �������� ������
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // ����� ����� cookie
        options.SlidingExpiration = true; // ����������� ������� ����� cookie ��� ���������� ������������
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

// ��������� ��� ������ ����� `app.UseRouting()`
app.UseSession();
app.UseRouting();

// ��������� middleware ��� �������������� � �����������
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    //  pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
