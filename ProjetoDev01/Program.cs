using Microsoft.EntityFrameworkCore;
using ProjetoDev1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Instalar pelo comando Install-Package Npgsql.EntityFrameworkCore.PostgreSQL
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<BancoDados>(op =>
op.UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=PROJETO_DEVdb;User Id=postgres;Password=root;"));

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
