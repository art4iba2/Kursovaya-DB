using Microsoft.EntityFrameworkCore;
using skress.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы в контейнер.
// Аутентификация осуществляется по куки
builder.Services.AddControllersWithViews();
builder.Services.AddSession();// Добавляем поддержку сессий

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();// Активируем сессии

app.UseAuthorization();

// Определение маршрутов контроллеров по умолчанию.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Privacy}/{id?}");

app.Run();