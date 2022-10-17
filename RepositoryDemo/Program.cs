using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositoryDemo.Context;
using RepositoryDemo.Entity;
using RepositoryDemo.Extentions;
using RepositoryDemo.Seeds;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
// Add services to the container.

services.AddDependencies();
services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

services.AddControllersWithViews().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

services.AddIdentity<User, IdentityRole>(identityOption =>
{
    identityOption.Password.RequiredLength = 7;
    identityOption.Password.RequireNonAlphanumeric = true;
    identityOption.Password.RequireLowercase = true;
    identityOption.Password.RequireUppercase = true;
    identityOption.Password.RequireDigit = true;

    identityOption.User.RequireUniqueEmail = true;

    identityOption.Lockout.MaxFailedAccessAttempts = 5;
    identityOption.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    identityOption.Lockout.AllowedForNewUsers = true;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


using (var scope = app.Services.CreateScope())
{
    var servicesProvider = scope.ServiceProvider;
    try
    {
        var userManager = servicesProvider.GetRequiredService<UserManager<User>>();
        var roleManager = servicesProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultSuperAdmin.SeedAsync(userManager, roleManager);
        await DefaultBasicUser.SeedAsync(userManager, roleManager);
        Console.WriteLine("Finished Seeding Default Data");
        Console.WriteLine("Application Starting");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message + "  :An error occurred seeding the DB");
    }
    finally
    {
    }
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();