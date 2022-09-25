using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryDemo.Dtos;

public class ProductCreateDto
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    [NotMapped] [Required] public IFormFile Photo { get; set; }

    public int CategoryId { get; set; }
}