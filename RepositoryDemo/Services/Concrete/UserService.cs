using RepositoryDemo.Entity;
using RepositoryDemo.Repository.Abstract;
using RepositoryDemo.Results;
using RepositoryDemo.Services.Abstract;

namespace RepositoryDemo.Services.Concrete;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<IDataResult<User>> GetDefaultUser()
    {
        var defaultUser = await _userRepository.GetDefaultUser();
        return new SuccessDataResult<User>(defaultUser, 200, "success");
    }
}