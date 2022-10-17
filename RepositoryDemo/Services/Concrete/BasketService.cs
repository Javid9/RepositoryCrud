using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepositoryDemo.Entity;
using RepositoryDemo.Repository.Abstract;
using RepositoryDemo.Results;
using RepositoryDemo.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RepositoryDemo.Dtos;
using IResult = RepositoryDemo.Results.IResult;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace RepositoryDemo.Services.Concrete;

public class BasketService : IBasketService
{
    private readonly IProductRepository _productRepository;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BasketService(IProductRepository productRepository, UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _productRepository = productRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    // Index
    public async Task<IDataResult<ResponseBasketIndexDto>> BasketIndex()
    {
        decimal productTotalPrice = 0;
        var httpContext = _httpContextAccessor.HttpContext!;
        var currentUser = _httpContextAccessor.HttpContext!.User.Identity!;

        string basketJson = httpContext.Request.Cookies["basket"]!;
        
        List<BasketDto> baskets = new();
        List<BasketDto> userProducts = new List<BasketDto>();

        if (baskets != null)
            
        {
            baskets = JsonConvert.DeserializeObject<List<BasketDto>>(basketJson);

            foreach (var basketProduct in baskets.ToList())
            {
                if (basketProduct.UserName == currentUser.Name)
                {
                    var dbProduct = _productRepository
                        .Table
                        .Include(p => p.Category)
                        .FirstOrDefault(x => x.Id == basketProduct.Id);
                    if (dbProduct != null)
                    {
                        basketProduct.Price = dbProduct.Price;
                        basketProduct.Title = dbProduct.Title;
                        basketProduct.Name = dbProduct.Name;
                        basketProduct.Image = dbProduct.Image;
                        basketProduct.Quantity = dbProduct.Quantity;
                    }
                    userProducts.Add(basketProduct);
                }
                basketProduct.ProductTotalPrice = basketProduct.BasketCount * basketProduct.Price;
                productTotalPrice += basketProduct.ProductTotalPrice;
            }
            
        }

        var responseBasketIndexDto = new ResponseBasketIndexDto
        {
            Products = userProducts,
            ProductTotalPrice = productTotalPrice
        };


        return new SuccessDataResult<ResponseBasketIndexDto>(responseBasketIndexDto, 200);
    }

   // Add Basket

 
  
 
    public async Task<IResult> AddBasket(int? id, int addProductCount)
    {
        decimal basketTotalPrice = 0;
        if (id is null) return new ErrorResult(400, "Id is null");
    
        var dbProduct = await _productRepository.Get(x => x.Id == id);
    
        if (dbProduct == null!) return new ErrorResult(404, "Product not found");
    
        var httpContext = _httpContextAccessor.HttpContext!;
        var currentUser = _httpContextAccessor.HttpContext!.User.Identity!;
        
    
        var jsonBasket = httpContext.Request.Cookies["basket"];
    
        List<BasketDto> baskets = null!;
    
        if (httpContext.Request.Cookies["basket"] == null)
        {
            baskets = new List<BasketDto>();
        }
        else
        {
            baskets = JsonSerializer.Deserialize<List<BasketDto>>(jsonBasket!)!;
        }
    
    
        var existProduct = baskets.FirstOrDefault(p => p.Id == id && p.UserName == currentUser.Name)!;

        if (existProduct == null!)
        {
            BasketDto basketDto = new()
            {
                Id = dbProduct.Id,
                UserName = currentUser.Name!,
                BasketCount = 1,
            };
            if (addProductCount != 0)
            {
                basketDto.BasketCount = addProductCount;
            }

            baskets.Add(basketDto);
        } else
        {
            existProduct.BasketCount++;
        }
        
    
        foreach (var basketProduct in baskets.Where(x => x.UserName == currentUser.Name))
        {
            dbProduct = _productRepository.Table.Include(p => p.Category)
                .FirstOrDefault(x => x.Id == basketProduct.Id);
    
            if (dbProduct != null)
            {
                basketProduct.Price = dbProduct.Price;
                basketProduct.Title = dbProduct.Name;   
            }
            
        }
        
    
        var json = JsonSerializer.Serialize(baskets);

        httpContext.Response.Cookies.Append("basket", json,
            new CookieOptions { MaxAge = TimeSpan.FromDays(14) });


        return new SuccessResult(201, "Product added to basket");
    }


    
    // Basket Count
    
    public async Task<IDataResult<ProductCountPlusDto>> ProductCountPlus(int id)
    {
        int basketProductDbCount = 0;
        decimal basketTotalPrice = 0;
        decimal productTotalPrice = 0;
    
        var httpContext = _httpContextAccessor.HttpContext!;
        var currentUser = _httpContextAccessor.HttpContext!.User.Identity!;
        var basket = httpContext.Request.Cookies["basket"];    
        
        List<BasketDto> basketProducts = JsonConvert.DeserializeObject<List<BasketDto>>(basket);
        var product = basketProducts.FirstOrDefault(x => x.Id == id && x.UserName == currentUser.Name);
        product.BasketCount++;
        int basketCount = product.BasketCount;
        
        
        foreach (var basketProduct in basketProducts.Where(x => x.UserName == currentUser.Name))
        {
            var dbProduct = _productRepository.Table.FirstOrDefault(x => x.Id == basketProduct.Id);
    
            if (dbProduct != null)
            {
                basketProduct.Price = dbProduct.Price;
                basketProduct.Name = dbProduct.Name;
                basketProduct.Quantity = dbProduct.Quantity;
            }
    
            if (dbProduct.Id == id)
            {
                basketProductDbCount = dbProduct.Quantity;
            }
            
            
            basketProduct.ProductTotalPrice = basketProduct.BasketCount * basketProduct.Price;
            if (basketProduct.Id == id)
            {
                productTotalPrice = basketProduct.ProductTotalPrice;
            }
            basketTotalPrice += basketProduct.ProductTotalPrice;
        }
    
        string xBasket = JsonConvert.SerializeObject(basketProducts);
        httpContext.Response.Cookies.Append("basket", xBasket, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });
        var productCountPlusDto = new ProductCountPlusDto
        {
            BasketProducts = basketProducts,
            ProductBasketCount = basketCount,
            BasketTotalPrice = basketTotalPrice,
            ProductTotalPrice = productTotalPrice,
            BasketProductDbCount = basketProductDbCount,
            ProductId = product.Id
        };


        return new SuccessDataResult<ProductCountPlusDto>(productCountPlusDto,200,"success");

    }
    
    
    // Product Plus
    public async Task<IDataResult<ProductCountMinusDto>> ProductCountMinus(int id)
    {
            decimal basketTotalPrice = 0;
            decimal productTotalPrice = 0;
            var httpContext = _httpContextAccessor.HttpContext!;
            var currentUser = _httpContextAccessor.HttpContext!.User.Identity!;
            
            string basket = httpContext.Request.Cookies["basket"];
            List<BasketDto> basketProducts = JsonConvert.DeserializeObject<List<BasketDto>>(basket);
            BasketDto product = basketProducts.FirstOrDefault(p => p.Id == id);

            if (product.BasketCount > 1)
            {
                product.BasketCount--;
            }
            else
            {
                basketProducts.Remove(product);
            }

            
            int basketCount = product.BasketCount;
            foreach (var basketProduct in basketProducts.Where(x => x.UserName == currentUser.Name))
            {
                Product dbProduct = _productRepository.Table.FirstOrDefault(x => x.Id == basketProduct.Id);
                if (dbProduct != null)
                {
                    basketProduct.Price = dbProduct.Price;
                    basketProduct.Title = dbProduct.Name;
                    basketProduct.Quantity = dbProduct.Quantity;
                }
                basketProduct.ProductTotalPrice = basketProduct.BasketCount * basketProduct.Price;
                if (basketProduct.Id == id)
                {
                    productTotalPrice = basketProduct.ProductTotalPrice;
                }
                basketTotalPrice += basketProduct.ProductTotalPrice;
            }

            string xbasket = JsonConvert.SerializeObject(basketProducts);
            httpContext.Response.Cookies.Append("basket", xbasket, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });
            
            var productMinusDto = new ProductCountMinusDto()
            {
                BasketProducts = basketProducts,
                ProductBasketCount = basketCount,
                BasketTotalPrice = basketTotalPrice,
                ProductTotalPrice = productTotalPrice
            };


            return new SuccessDataResult<ProductCountMinusDto>(productMinusDto, 200, "success");
            
    }
    
    
    
    

    public async Task<IDataResult<BasketRemoveDto>> RemoveProduct(int id)
    {
        decimal basketTotalPrice = 0;
        
        var httpContext = _httpContextAccessor.HttpContext!;
        var currentUser = _httpContextAccessor.HttpContext!.User.Identity!;
        
        string basket = httpContext.Request.Cookies["basket"];
        List<BasketDto> basketProducts = JsonConvert.DeserializeObject<List<BasketDto>>(basket);
            
        BasketDto product = basketProducts.Where(p => p.UserName == currentUser.Name)
            .FirstOrDefault(p => p.Id == id);
        basketProducts.Remove(product);
        foreach (var basketProduct in basketProducts.Where(p=>p.UserName==currentUser.Name))
        {
            basketProduct.ProductTotalPrice = basketProduct.BasketCount * basketProduct.Price;
            basketTotalPrice += basketProduct.ProductTotalPrice;
        }
            
        string xbasket = JsonConvert.SerializeObject(basketProducts);
        httpContext.Response.Cookies.Append("basket", xbasket, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });
        var removeBasketDto = new BasketRemoveDto()
        {
            BasketTotalPrice = basketTotalPrice,
            BasketProductCount = basketProducts.Where(x=>x.UserName == currentUser.Name).ToList()
        };


        return new SuccessDataResult<BasketRemoveDto>(removeBasketDto, 200, "success");

    }
}





    

