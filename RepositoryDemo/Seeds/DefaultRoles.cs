using Microsoft.AspNetCore.Identity;
using RepositoryDemo.Entity;

namespace RepositoryDemo.Seeds;

public static class DefaultRoles
{
    public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Roles
        await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        await roleManager.CreateAsync(new IdentityRole("Admin"));
        await roleManager.CreateAsync(new IdentityRole("Moderator"));
        await roleManager.CreateAsync(new IdentityRole("Basic"));
    }
}