namespace RepositoryDemo.Entity;

public class User : BaseEntity
{
    public string? FullName { get; set; }
    public string? Email { get; set; }

    public ICollection<Order> Orders { get; set; } = null!;
}