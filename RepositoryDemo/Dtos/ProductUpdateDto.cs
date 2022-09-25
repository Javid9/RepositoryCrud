using RepositoryDemo.Entity;

namespace RepositoryDemo.Dtos;

public class ProductUpdateDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public IFormFile Photo { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
}