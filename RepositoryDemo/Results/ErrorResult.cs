namespace RepositoryDemo.Results;

public class ErrorResult : Result
{
    public ErrorResult(int statusCode, string message) : base(false, statusCode, message)
    {
    }

    public ErrorResult(int statusCode) : base(false, statusCode)
    {
    }
}