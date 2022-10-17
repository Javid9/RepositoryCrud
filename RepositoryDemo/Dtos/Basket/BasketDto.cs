using RepositoryDemo.Entity;

namespace RepositoryDemo.Dtos;

public class BasketDto
{
    
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Title { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Image { get; set; }
    public int BasketCount { get; set; } = 1;
    
    public int TotalBasketCount { get; set; }
    public decimal ProductTotalPrice { get; set; }
    public string UserName { get; set; }
    
    public Category Category { get; set; }
}