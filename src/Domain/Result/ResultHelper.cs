namespace PalpiteApi.Domain.Result;

public static class ResultHelper
{
    public static Result Success() => new(true, default!);
    public static Result Failure(Error error) => new(false, error);
    public static Result<T> Success<T>(T value) => new(value, true, default!);
    public static Result<T> Failure<T>(Error error) => new(default!, false, error);
}