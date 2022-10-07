using Microsoft.AspNetCore.Mvc;
using RepositoryDemo.Dtos;
using RepositoryDemo.Services.Abstract;

namespace RepositoryDemo.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var result = await _userService.Register(registerDto);

        if (!result.Success)
        {
            ModelState.AddModelError("",result.Message);
            return View(registerDto);
        }
        
        return RedirectToAction("Index","Home");
    }

    
    

    public IActionResult Login()
    {
        return View();
    }





    [HttpPost]
      public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result =  await _userService.Login(loginDto);

        if (!result.Success)
        {
            ModelState.AddModelError("",result.Message);
            return View();
        }

        return RedirectToAction("Index", "Home");
    }


}