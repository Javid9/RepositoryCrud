namespace RepositoryDemo.Dtos;

public class ProductCountPlusDto
{
    public List<BasketDto> BasketProducts { get; set; }
    public int ProductBasketCount { get; set; }
    public decimal BasketTotalPrice { get; set; }
    public decimal ProductTotalPrice { get; set; }
    public int BasketProductDbCount { get; set; }
    public int ProductId { get; set; }
}