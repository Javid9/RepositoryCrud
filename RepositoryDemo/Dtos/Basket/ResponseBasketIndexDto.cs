namespace RepositoryDemo.Dtos;

public class ResponseBasketIndexDto
{
    public List<BasketDto> Products { get; set; }
    public decimal ProductTotalPrice { get; set; }
}