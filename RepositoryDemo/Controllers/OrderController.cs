using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RepositoryDemo.Services.Abstract;
using RepositoryDemo.ViewModels;

namespace RepositoryDemo.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;
        
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    
    // GET All
    public async Task<IActionResult> Index()
    {
        var result = await _orderService.GetAll();
        return View(result.Data);
    }


    //Create
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] List<int> productList)
    {
        var result = await _orderService.Add(productList);

        return RedirectToAction("Index");
    }


    //User Info Get
    public async Task<IActionResult> UserInfo(int productId)
    {
        ViewBag.productId = productId;
        return View();
    }


    //User Info Post
    [HttpPost]
    public async Task<IActionResult> UserInfo(CreateOrderByUserDto createOrderByUserDto, int productId)
    {
        var result = await _orderService.CreateOrderByUser(createOrderByUserDto, productId);
        
        (string message, bool success) msg;
        
        if (!result.Success)
        {
            // ModelState.AddModelError("errorMessage", result.Message);
            msg = Notify(result.Success, result.Message);

            return RedirectToAction("UserInfo", msg);
        }

        msg = Notify(result.Success, result.Message);


        return RedirectToAction("UserInfo", msg);
    }

    
    
    // Alert
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