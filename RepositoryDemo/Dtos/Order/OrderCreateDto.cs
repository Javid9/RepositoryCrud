using RepositoryDemo.Entity;

namespace RepositoryDemo.Dtos;

public class OrderCreateDto
{
    public DateTime OrderDate { get; set; }

    public User User { get; set; } = null!;
    public int UserId { get; set; }

    public ICollection<ProductOrder> ProductOrders { get; set; } = null!;
}