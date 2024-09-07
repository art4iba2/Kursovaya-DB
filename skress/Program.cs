using Microsoft.EntityFrameworkCore;
using skress.Models;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������� � ���������.
// �������������� �������������� �� ����
builder.Services.AddControllersWithViews();
builder.Services.AddSession();// ��������� ��������� ������

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

app.UseSession();// ���������� ������

app.UseAuthorization();

// ����������� ��������� ������������ �� ���������.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Privacy}/{id?}");

app.Run();