using RepositoryDemo.Dtos;
using IResult = RepositoryDemo.Results.IResult;

namespace RepositoryDemo.Services.Abstract;

public interface IUserService
{ 
    Task<IResult> Register(RegisterDto registerDto);
}