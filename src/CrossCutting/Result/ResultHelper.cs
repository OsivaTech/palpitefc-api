namespace PalpiteFC.Api.CrossCutting.Result;

public static class ResultHelper
{
    public static Result Success() => new(true);
    public static Result Success(Message message) => new(true, [message]);
    public static Result Success(List<Message> messages) => new(true, messages);

    public static Result<T> Success<T>(T value) => new(value, true);
    public static Result<T> Success<T>(T value, Message message) => new(value, true, [message]);
    public static Result<T> Success<T>(T value, List<Message> messages) => new(value, true, messages);

    public static Result Failure(Message message) => new(false, [message]);
    public static Result Failure(List<Message> errors) => new(false, errors);

    public static Result<T> Failure<T>(Message message) => new(default!, false, [message]);
    public static Result<T> Failure<T>(List<Message> errors) => new(default!, false, errors);
}