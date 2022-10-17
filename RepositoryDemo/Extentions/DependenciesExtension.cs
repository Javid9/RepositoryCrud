using RepositoryDemo.Dtos;
using RepositoryDemo.Repository.Abstract;
using RepositoryDemo.Repository.Concrete;
using RepositoryDemo.Services.Abstract;
using RepositoryDemo.Services.Concrete;

namespace RepositoryDemo.Extentions;

public static class DependenciesExtension
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();


        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();


        services.AddScoped<IProductOrderRepository, ProductOrderRepository>();
        
        services.AddScoped<IBasketService, BasketService>();
    }
}