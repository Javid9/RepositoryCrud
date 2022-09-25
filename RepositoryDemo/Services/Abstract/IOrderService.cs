using RepositoryDemo.Dtos;
using RepositoryDemo.Entity;
using RepositoryDemo.Results;

namespace RepositoryDemo.Services.Abstract;

public interface IOrderService
{
    Task<IDataResult<List<Order>>> GetAll();

    Task<IDataResult<Order>> Add( List<int> list);
    // Task<IResult> UpdateCategory();
    // Task Delete(int id);
}