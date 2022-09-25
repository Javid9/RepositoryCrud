namespace RepositoryDemo.Results;

public interface IResult
{
    bool Success { get; }

    string Message { get; }

    int StatusCode { get; }
}