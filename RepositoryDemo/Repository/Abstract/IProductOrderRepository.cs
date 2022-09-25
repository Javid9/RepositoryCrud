
using RepositoryDemo.Entity;
using IResult = RepositoryDemo.Results.IResult;

namespace RepositoryDemo.Repository.Abstract;

public interface IProductOrderRepository:IGenericRepository<ProductOrder>
{
    Task<IResult> AddProductOrders(List<ProductOrder> productOrders);
}