using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RepositoryDemo.Services.Abstract;
using RepositoryDemo.ViewModels;

namespace RepositoryDemo.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;

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
    public async Task<IActionResult> Create([FromForm] List<int> productList)
    {
        var result = await _orderService.Add(productList);

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> UserInfo(int productId)
    {
        ViewBag.productId = productId;

        TempData["id"] = JsonSerializer.Serialize(productId);

        return View();
    }


    [HttpPost]
    public async Task<IActionResult> UserInfo(CreateOrderByUserDto createOrderByUserDto, int productId)
    {
        var result = await _orderService.CreateOrderByUser(createOrderByUserDto, productId);
        var msg = Notify(result.Success, result.Message);
        if (!result.Success)
        {
            ModelState.AddModelError("errorMessage", result.Message);
            return RedirectToAction("UserInfo");
        }

        
        return RedirectToAction("UserInfo", msg);
    }

    
    private (string message, bool success) Notify(bool success, string message)
    {
        var msg = new
        {
            Message = message,
            Success = success,
        };

        TempData["notification"] = JsonSerializer.Serialize(msg);

        return (msg.Message, msg.Success);
    }
}