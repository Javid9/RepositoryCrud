namespace RepositoryDemo.Entity;

public class Order : BaseEntity
{
    public DateTime OrderDate { get; set; } 

    public User User { get; set; } = null!;
    public int UserId { get; set; }

    public ICollection<ProductOrder> ProductOrders { get; set; } = null!;

    public Order()
    {
        OrderDate = new DateTime();
    }
}