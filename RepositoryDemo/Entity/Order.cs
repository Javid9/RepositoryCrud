namespace RepositoryDemo.Entity;

public class Order : BaseEntity
{
    public Order()
    {
        OrderDate = new DateTime();
    }

    public DateTime OrderDate { get; set; }

    public User User { get; set; } = null!;
    public int UserId { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public ICollection<ProductOrder> ProductOrders { get; set; } = null!;
}