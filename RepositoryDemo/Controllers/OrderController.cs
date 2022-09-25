using Microsoft.AspNetCore.Mvc;
using RepositoryDemo.Dtos;
using RepositoryDemo.Services.Abstract;

namespace RepositoryDemo.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;

    // GET
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }


    public async Task<IActionResult> Index()
    {
        var result = await _orderService.GetAll();
        return View(result.Data);
    }


   

    [HttpPost]
    public async Task<IActionResult> Create( [FromForm] List<int> productList)
    {
        var result = await _orderService.Add(productList);
        
        return RedirectToAction("Index");
    }


   
}