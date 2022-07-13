using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using TBGestionStock.ASP.Models;
using TBGestionStock.ASP.Services;
using TBGestionStock.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StockContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("MainStock")));
builder.Services.AddScoped<ProduitService>();
builder.Services.AddScoped<MailService>();
builder.Services.AddScoped<SmtpClient>();
builder.Services.AddScoped<CategorieService>();
builder.Services.AddSingleton(builder.Configuration.GetSection("SMTP").Get<MailConfigurVM>());
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
    pattern: "{controller=Produit}/{action=Index}/{id?}");

app.Run();
