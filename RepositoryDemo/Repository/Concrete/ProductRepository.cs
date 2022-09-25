using RepositoryDemo.Context;
using RepositoryDemo.Entity;
using RepositoryDemo.Repository.Abstract;

namespace RepositoryDemo.Repository.Concrete;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
}