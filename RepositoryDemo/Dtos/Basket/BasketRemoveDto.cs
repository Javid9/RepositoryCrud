namespace RepositoryDemo.Dtos;

public class BasketRemoveDto
{
    public List<BasketDto> BasketProductCount   { get; set; }
    public decimal BasketTotalPrice  { get; set; }
}