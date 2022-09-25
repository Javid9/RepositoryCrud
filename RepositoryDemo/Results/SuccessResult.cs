namespace RepositoryDemo.Results;

public class SuccessResult : Result
{
    public SuccessResult(int statusCode, string message) : base(true, statusCode, message)
    {
    }

    public SuccessResult(int statusCode) : base(true, statusCode)
    {
    }
}