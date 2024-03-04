namespace PalpiteApi.Domain.Result;

public static class Result
{
    public static Result<T> Success<T>(T value) => new(value, true, default!);
    public static Result<T> Failure<T>(Error error) => new(default!, false, error);
}

public class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T Value { get; }
    public Error Error { get; }

    public Result(T value, bool isSuccess, Error error)
    {
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
    }
}
