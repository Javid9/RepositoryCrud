using RepositoryDemo.Dtos;
using RepositoryDemo.Entity;
using RepositoryDemo.Results;
using IResult = RepositoryDemo.Results.IResult;

namespace RepositoryDemo.Services.Abstract;

public interface IProductService
{
    Task<IDataResult<List<Product>>> GetAll();
    Task<IDataResult<Product>> Get(int id);
    Task<IDataResult<Product>> Details(int id);
    Task<IResult> Add(ProductCreateDto productCreateDto);

    Task<IResult> UpdateProduct(ProductUpdateDto productUpdateDto);

    Task Delete(int id);
    // Task<string> Upload(IFormFile file, string path);
}