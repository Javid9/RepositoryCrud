using Microsoft.AspNetCore.Identity;
using RepositoryDemo.Dtos;
using RepositoryDemo.Entity;
using RepositoryDemo.Results;
using RepositoryDemo.Services.Abstract;
using IResult = RepositoryDemo.Results.IResult;

namespace RepositoryDemo.Services.Concrete;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }


    public async Task<IResult> Register(RegisterDto registerDto)
    {
        if (_userManager.Users.Any(x => x.Email == registerDto.Email || x.UserName==registerDto.UserName))
            return new ErrorResult(400, "Email already taken");
        
        User newUser = new()
        {
            Email = registerDto.Email,
            UserName = registerDto.UserName,
            FullName = registerDto.FullName,
        };

        var identityResult = await _userManager.CreateAsync(newUser, registerDto.Password);

        if (!identityResult.Succeeded)
        {
            return new ErrorResult(400,"Bad request");
        }

        await _userManager.AddToRoleAsync(newUser, "Basic");
        await _signInManager.SignInAsync(newUser, true);

        return new SuccessResult(200, "User Created");
    }

    
    public async Task<IResult> Login(LoginDto loginDto)
    {
        var loginUser = await _userManager.FindByEmailAsync(loginDto.Email);
        if (loginUser == null)
        {
            return new ErrorResult(400, "Email or Password wrong");
        }

        
        
        var signInResult = await _signInManager.PasswordSignInAsync(loginUser,loginDto.Password,true,false);

        if (signInResult.IsLockedOut)
        {
            return new ErrorResult(400, "The account is locked out");
        }


        if (!signInResult.Succeeded)
        {
            return new ErrorResult(400, "Email or password wrong");
        }
        

        return new SuccessResult(200, "ok");
    }
}