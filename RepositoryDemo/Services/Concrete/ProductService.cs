using Microsoft.EntityFrameworkCore;
using RepositoryDemo.Dtos;
using RepositoryDemo.Entity;
using RepositoryDemo.Extentions;
using RepositoryDemo.Repository.Abstract;
using RepositoryDemo.Results;
using RepositoryDemo.Services.Abstract;
using IResult = RepositoryDemo.Results.IResult;

namespace RepositoryDemo.Services.Concrete;

public class ProductService : IProductService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IWebHostEnvironment _env;
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository,
        IWebHostEnvironment env)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _env = env;
    }


    public async Task<IDataResult<List<Product>>> GetAll()
    {
        var products = await _productRepository.GetAll(null, new[] { "Category" });
        if (products.Count is 0) return new ErrorDataResult<List<Product>>(products, 404, "Not Found");
        return new SuccessDataResult<List<Product>>(products, 200);
    }

    public async Task<IDataResult<Product>> Get(int id)
    {
        var result = await _productRepository.Get(x => x.Id == id, new[] { "Category" });
        if (result is null) return new ErrorDataResult<Product>(result, 404, "Not Found");
        return new SuccessDataResult<Product>(result, 200);
    }

    public async Task<IDataResult<Product>> Details(int id)
    {
        var result = await _productRepository.Table.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        return new SuccessDataResult<Product>(result, 200, "success");
    }


    public async Task<IResult> Add(ProductCreateDto productCreateDto)
    {
        var result = await _categoryRepository.Get(x => x.Id == productCreateDto.CategoryId);

        if (productCreateDto.Photo is null) return new ErrorResult(400, "File not found");

        if (result is null) return new ErrorResult(404, "Category not found");


        var path = Path.Combine("image");
        var fileName = await productCreateDto.Photo.SaveImg(_env.WebRootPath, path);

        if (fileName == null) throw new Exception("Please add file");


        Product product = new()
        {
            Name = productCreateDto.Name,
            Price = productCreateDto.Price,
            Quantity = productCreateDto.Quantity,
            Category = result
        };

        product.Image = fileName;


        await _productRepository.Add(product);
        return new SuccessResult(201, "Product successfully created");
    }


    public async Task<IResult> UpdateProduct(ProductUpdateDto productUpdateDto)
    {
        var result = await _productRepository.Table.Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == productUpdateDto.Id);


        if (productUpdateDto.Photo is null) return new ErrorResult(400, "File not found");

        var path = Path.Combine("image");
        var fileName = await productUpdateDto.Photo.SaveImg(_env.WebRootPath, path);

        result.Name = productUpdateDto.Name;
        result.Price = productUpdateDto.Price;
        result.Quantity = productUpdateDto.Quantity;
        result.CategoryId = productUpdateDto.CategoryId;

        result.Image = fileName;

        await _productRepository.Update(result);
        return new SuccessResult(201, "Product successfully created");
    }


    public async Task Delete(int id)
    {
        var result = await _productRepository.Table.FirstOrDefaultAsync(x => x.Id == id);

        await _productRepository.Delete(result);
    }
}