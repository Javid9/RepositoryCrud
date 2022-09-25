using Microsoft.EntityFrameworkCore;
using RepositoryDemo.Dtos;
using RepositoryDemo.Entity;
using RepositoryDemo.Repository.Abstract;
using RepositoryDemo.Results;
using RepositoryDemo.Services.Abstract;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace RepositoryDemo.Services.Concrete;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductOrderRepository _productOrderRepository;

    public OrderService(IOrderRepository orderRepository, IProductOrderRepository productOrderRepository)
    {
        _orderRepository = orderRepository;
        _productOrderRepository = productOrderRepository;
    }

    public async Task<IDataResult<List<Order>>> GetAll()
    {
        var result = await _orderRepository.Table
            .Include(x => x.User)
            .Include(x => x.ProductOrders)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Category).ToListAsync();

        if (result.Count is 0) return new ErrorDataResult<List<Order>>(result, 404, "Not Found");
        return new SuccessDataResult<List<Order>>(result, 200);
    }


    public async Task<IDataResult<Order>> Add(List<int> productList)
    {
        
        
        Order newOrder = new Order()
        {
            UserId = 1
        };

        var order = await _orderRepository.Add(newOrder);
        
        
        List<ProductOrder> productOrders = new List<ProductOrder>();
        foreach (var productId in productList)
        {
            ProductOrder productOrder = new ProductOrder()
            {
                ProductId = productId,
                OrderId = newOrder.Id
            };
            
            productOrders.Add(productOrder);
        }

        await _productOrderRepository.AddProductOrders(productOrders);
        
        
        
        
        return new DataResult<Order>(newOrder, true, 201, "Order successfully save");
        
    }

}