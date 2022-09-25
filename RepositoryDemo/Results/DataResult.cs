namespace RepositoryDemo.Results;

public class DataResult<T> : Result, IDataResult<T>
{
    public DataResult(T data, bool success, int statusCode, string message) : base(success, statusCode, message)
    {
        Data = data;
    }

    public DataResult(T data, bool success, int statusCode) : base(success, statusCode)
    {
        Data = data;
    }

    public T Data { get; }
}