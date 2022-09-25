using RepositoryDemo.Dtos;
using RepositoryDemo.Entity;
using RepositoryDemo.Repository.Abstract;
using RepositoryDemo.Results;
using RepositoryDemo.Services.Abstract;
using IResult = RepositoryDemo.Results.IResult;

namespace RepositoryDemo.Services.Concrete;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }


    public async Task<List<Category>> GetAll()
    {
        var result = await _categoryRepository.GetAll(null, new[] { "Products" });
        // if (result.Count == 0) return new ErrorDataResult<List<Category>>(result, 404, "Category Not Found");
        // return new SuccessDataResult<List<Category>>(result, 200);
        return result;
    }


    public async Task<IResult> Add(CategoryCreateDto categoryCreateDto)
    {
        Category category = new()
        {
            Name = categoryCreateDto.Name
        };

        await _categoryRepository.Add(category);
        return new SuccessResult(201, "Category successfully added");
    }


    public async Task<IResult> UpdateCategory(CategoryUpdateDto updateCategory)
    {
        var result = await _categoryRepository.Get(x => x.Id == updateCategory.Id);
        result.Name = updateCategory.Name;
        await _categoryRepository.Update(result);
        return new ErrorResult(400, "Bad Request");
    }


    public async Task Delete(int id)
    {
        var result = await _categoryRepository.Get(x => x.Id == id);
        await _categoryRepository.Delete(result);
    }
}