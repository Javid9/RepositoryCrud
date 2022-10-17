using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RepositoryDemo.Services.Abstract;

namespace RepositoryDemo.Controllers;

public class BasketController : Controller
{
    private readonly IBasketService _basketService;

    // GET
    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    
    //Get
    public async Task<IActionResult> Index()
    {
        if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
        var result = await _basketService.BasketIndex();
        
        ViewBag.BasketTotalPrice = result.Data.ProductTotalPrice;
        
        return View(result.Data.Products);
    }
    
    
    // Add
    public async Task<IActionResult> AddBasket([FromForm] int id, int addProductCount)
    {
        var result = await _basketService.AddBasket(id, addProductCount);

        if (result.Success) return RedirectToAction("Index");

        return View("Index");
    }


    
    //Plus
    public async Task<IActionResult> ProductCountPlus([FromForm] int id)
    {
        var result = _basketService.ProductCountPlus(id);
        
        return Ok(result);
    }
    
    

    //Minus
    public async Task<IActionResult> ProductCountMinus([FromForm] int id)
    {
        var result = _basketService.ProductCountMinus(id);
        
        return Ok(result);
    }


    //Remove
    public async Task<IActionResult> RemoveProduct(int id)
    {
        var result = _basketService.RemoveProduct(id);

        return Ok(result);
    }








}