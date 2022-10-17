using RepositoryDemo.Entity;

namespace RepositoryDemo.Results;

public interface IDataResult<out T> : IResult
{
    T Data { get; }
}