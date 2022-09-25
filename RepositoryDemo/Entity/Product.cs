using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryDemo.Entity;

public class Product : BaseEntity
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public string Image { get; set; }

    [NotMapped] [Required] public IFormFile Photo { get; set; }


    public ICollection<ProductOrder> ProductOrders { get; set; } = null!;
}