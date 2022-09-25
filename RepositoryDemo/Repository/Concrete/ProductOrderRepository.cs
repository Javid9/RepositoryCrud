using RepositoryDemo.Context;
using RepositoryDemo.Entity;
using RepositoryDemo.Repository.Abstract;
using RepositoryDemo.Results;
using IResult = RepositoryDemo.Results.IResult;

namespace RepositoryDemo.Repository.Concrete;

public class ProductOrderRepository : GenericRepository<ProductOrder>, IProductOrderRepository
{
    private readonly AppDbContext _appDbContext;
    public ProductOrderRepository(AppDbContext context) : base(context)
    {
        _appDbContext = context;
    }

    public async Task<IResult> AddProductOrders(List<ProductOrder> productOrders)
    {
        await _appDbContext.ProductOrders.AddRangeAsync(productOrders);
        await _appDbContext.SaveChangesAsync();
        return new SuccessResult(201,"Ok");
    }
}