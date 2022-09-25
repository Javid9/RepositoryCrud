using RepositoryDemo.Context;
using RepositoryDemo.Entity;
using RepositoryDemo.Repository.Abstract;

namespace RepositoryDemo.Repository.Concrete;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}