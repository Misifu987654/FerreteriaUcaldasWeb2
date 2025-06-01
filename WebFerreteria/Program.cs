using System;
using WebFerreteria.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Habilita que el aplicativo se comunique con la base de datos
builder.Services.AddDbContext<FerreteriaDbContext>();

// Middleware
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(); 

var app = builder.Build();

app.UseSession(); // 

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
