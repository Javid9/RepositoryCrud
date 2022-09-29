using Microsoft.EntityFrameworkCore;
using RepositoryDemo.Context;
using RepositoryDemo.Entity;
using RepositoryDemo.Repository.Abstract;

namespace RepositoryDemo.Repository.Concrete;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext context) : base(context)
    {
        _appDbContext = context;
    }


    public async Task<User> GetDefaultUser()
    {
        var defaultUser = await _appDbContext.Users.FirstOrDefaultAsync();
        return defaultUser;
    }
}