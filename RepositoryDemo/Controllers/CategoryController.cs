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

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAll();
        return View(categories);
    }


    public IActionResult Create()
    {
        return View();
    }


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


    [HttpGet]
    public async Task<IActionResult> Update()
    {
        return View();
    }


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


    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.Delete(id);
        return RedirectToAction("Index");
    }
}