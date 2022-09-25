using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryDemo.Dtos;
using RepositoryDemo.Entity;
using RepositoryDemo.Services.Abstract;
using RepositoryDemo.ViewModels;

namespace RepositoryDemo.Controllers;

public class ProductController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }


    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAll();

        return View(products.Data);
    }


    public async Task<IActionResult> Add()
    {
        var categories = await _categoryService.GetAll();
        ViewData["categories"] = new SelectList(categories, "Id", "Name");
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Add(ProductCreateDto productCreateDto)
    {
        var result = await _productService.Add(productCreateDto);
        if (!result.Success)
        {
            ModelState.AddModelError("errorMessage",result.Message);
            return View();
        }

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Update(int id)
    {
        var categories = await _categoryService.GetAll();
        ViewData["categories"] = new SelectList(categories, "Id", "Name");
       
        var product = await _productService.Get(id);

        var productVm = new ProductViewModel
        {
            Product = product.Data,
        };
        return View(productVm.Product);
    }


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


    public async Task<IActionResult> Delete(int id)
    {
        await _productService.Delete(id);
        return RedirectToAction("Index");
    }
}