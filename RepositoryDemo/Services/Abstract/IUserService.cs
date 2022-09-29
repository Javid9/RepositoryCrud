using RepositoryDemo.Entity;
using RepositoryDemo.Results;

namespace RepositoryDemo.Services.Abstract;

public interface IUserService
{
    Task<IDataResult<User>> GetDefaultUser();
}