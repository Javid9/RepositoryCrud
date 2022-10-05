using Microsoft.AspNetCore.Identity;

namespace RepositoryDemo.Entity;

public class User : IdentityUser
{
    public string? FullName { get; set; }

    public ICollection<Order> Orders { get; set; } = null!;
}