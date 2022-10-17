using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryDemo.Dtos;
using RepositoryDemo.Services.Abstract;
using IResult= RepositoryDemo.Results.IResult;
namespace RepositoryDemo.Controllers;

public class ProductController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    public ProductController(IProductService productService, ICategoryService categoryService, IUserService userService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    
    
    
    //Get All
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAll();
        return View(products.Data);
    }


    
    
    //Details
    public async Task<IActionResult> Details(int id)
    {
        var product = await _productService.Details(id);
        return View(product.Data);
    }


    //Add Get
    public async Task<IActionResult> Add()
    {
        var categories = await _categoryService.GetAll();
        ViewData["categories"] = new SelectList(categories, "Id", "Name");
        return View();
    }


    //Add Post
    [HttpPost]
    public async Task<IActionResult> Add(ProductCreateDto productCreateDto)
    {
        var result = await _productService.Add(productCreateDto);
        if (!result.Success)
        {
            ModelState.AddModelError("errorMessage", result.Message);
            return View();
        }

        return RedirectToAction("Index");
    }


    // Update Get
    public async Task<IActionResult> Update(int id)
    {
        var categories = await _categoryService.GetAll();
        ViewData["categories"] = new SelectList(categories, "Id", "Name");

        var product = await _productService.Get(id);

        
        return View(product.Data);
    }


    //Update Post
    [HttpPost]
    public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
    {
        var categories = await _categoryService.GetAll();
        ViewData["categories"] = new SelectList(categories, "Id", "Name");
        var result = await _productService.UpdateProduct(productUpdateDto);
        if (!result.Success)
        {
            ModelState.AddModelError("errorMessage", result.Message);
            return View();
        }

        return RedirectToAction("Index");
    }


    // Delete
    public async Task<IResult> Delete(int id)
    {
        return await _productService.Delete(id);
    }



    
    public async Task<IActionResult> ProductIndex()
    {
        var result = await _productService.ProductIndex();
        return View(result.Data);
    }
    
    
    
    
    public async Task<IActionResult> ProductDetail(int id)
    {
        
        var result = await _productService.ProductDetail(id);
        return View(result.Data);
    }
   



}