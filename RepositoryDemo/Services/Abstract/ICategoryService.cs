using RepositoryDemo.Dtos;
using RepositoryDemo.Entity;
using IResult = RepositoryDemo.Results.IResult;

namespace RepositoryDemo.Services.Abstract;

public interface ICategoryService
{
    Task<List<Category>> GetAll();
    Task<IResult> Add(CategoryCreateDto categoryCreateDto);
    Task<IResult> UpdateCategory(CategoryUpdateDto categoryUpdateDto);
    Task Delete(int id);
}