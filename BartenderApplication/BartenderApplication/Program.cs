using BartenderApplication.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BarDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BarDbContext>();
    if (!db.Cocktails.Any())
    {
        db.Cocktails.AddRange(
            new Cocktail { Name = "Mojito", Description = "A refreshing blend of white rum, mint, lime, sugar, and soda water.", Price = 10 },
            new Cocktail { Name = "Martini", Description = "Classic gin or vodka cocktail with dry vermouth, garnished with an olive or lemon twist.", Price = 12 },
            new Cocktail { Name = "Old Fashioned", Description = "Bourbon or rye whiskey, bitters, sugar, and orange peel.", Price = 11 },
            new Cocktail { Name = "Cosmopolitan", Description = "Vodka, triple sec, cranberry juice, and fresh lime juice.", Price = 11 },
            new Cocktail { Name = "Margarita", Description = "Tequila, triple sec, and lime juice served with a salted rim.", Price = 10 },
            new Cocktail { Name = "Negroni", Description = "Gin, Campari, and sweet vermouth, garnished with orange peel.", Price = 13 },
            new Cocktail { Name = "Whiskey Sour", Description = "Whiskey, lemon juice, sugar, and a dash of egg white.", Price = 10 },
            new Cocktail { Name = "Pina Colada", Description = "Rum, coconut cream, and pineapple juice, served blended or shaken.", Price = 12 }
        );
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
