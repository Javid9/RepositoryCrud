namespace RepositoryDemo.Results;

public class SuccessDataResult<T> : DataResult<T>
{
    public SuccessDataResult(T data, int statusCode, string message) : base(data, true, statusCode,
        message)
    {
    }

    public SuccessDataResult(T data, int statusCode) : base(data, true, statusCode)
    {
    }
}