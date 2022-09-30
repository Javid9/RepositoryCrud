using Microsoft.AspNetCore.Mvc;
using RepositoryDemo.Dtos;
using RepositoryDemo.Services.Abstract;

namespace RepositoryDemo.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    // GET
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    
    // Get All
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAll();
        return View(categories);
    }

    
    // Create Get
    public IActionResult Create()
    {
        return View();
    }


    //Create Post
    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateDto categoryCreateDto)
    {
        if (ModelState.IsValid)
        {
            var category = await _categoryService.Add(categoryCreateDto);
            return RedirectToAction("Index");
        }

        return View();
    }


    //Update Get
    [HttpGet]
    public async Task<IActionResult> Update()
    {
        return View();
    }


    //Update Post
    [HttpPost]
    public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.UpdateCategory(categoryUpdateDto);
            return RedirectToAction("Index");
        }

        return View();
    }


    //Delete
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.Delete(id);
        return RedirectToAction("Index");
    }
}