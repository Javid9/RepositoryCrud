using Microsoft.AspNetCore.Identity;
using RepositoryDemo.Entity;

namespace RepositoryDemo.Seeds;

public static class DefaultSuperAdmin
{
    public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Default User
        var defaultUser = new User
        {
            UserName = "superadmin",
            Email = "superadmin@gmail.com",
            FullName = "Mukesh",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };
        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                await userManager.AddToRoleAsync(defaultUser, "Basic");
                await userManager.AddToRoleAsync(defaultUser, "Moderator");
                await userManager.AddToRoleAsync(defaultUser, "Admin");
                await userManager.AddToRoleAsync(defaultUser, "SuperAdmin");
            }

        }
    }

}