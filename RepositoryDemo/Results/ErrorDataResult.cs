namespace RepositoryDemo.Results;

public class ErrorDataResult<T> : DataResult<T>
{
    public ErrorDataResult(T data, int statusCode, string message) : base(data, false, statusCode,
        message)
    {
    }

    public ErrorDataResult(T data, int statusCode) : base(data, false, statusCode)
    {
    }
}