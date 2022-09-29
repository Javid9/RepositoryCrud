using RepositoryDemo.Entity;
using RepositoryDemo.Results;
using RepositoryDemo.ViewModels;

namespace RepositoryDemo.Services.Abstract;

public interface IOrderService
{
    Task<IDataResult<List<Order>>> GetAll();

    Task<IDataResult<Order>> Add(List<int> list);

    Task<IDataResult<Order>> CreateOrderByUser(CreateOrderByUserDto userInfoViewModel, int productId);
    // Task<IResult> UpdateCategory();
    // Task Delete(int id);
}
