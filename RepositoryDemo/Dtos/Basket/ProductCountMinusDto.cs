namespace RepositoryDemo.Dtos;

public class ProductCountMinusDto
{
    public List<BasketDto> BasketProducts { get; set; }
    public int ProductBasketCount { get; set; }
    public decimal BasketTotalPrice { get; set; }
    public decimal ProductTotalPrice { get; set; }
}