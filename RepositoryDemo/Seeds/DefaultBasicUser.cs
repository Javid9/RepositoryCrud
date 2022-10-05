using Microsoft.AspNetCore.Identity;
using RepositoryDemo.Entity;

namespace RepositoryDemo.Seeds;

public static class DefaultBasicUser
{
    public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Default User
        var defaultUser = new User
        {
            UserName = "basicuser",
            Email = "basicuser@gmail.com",
            FullName = "Javid",
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
            }

        }
    }

}