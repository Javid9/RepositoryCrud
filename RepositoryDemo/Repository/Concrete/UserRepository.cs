using RepositoryDemo.Context;
using RepositoryDemo.Entity;
using RepositoryDemo.Repository.Abstract;

namespace RepositoryDemo.Repository.Concrete;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}