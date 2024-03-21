namespace PalpiteFC.Api.Domain.Result;

public class Result
{
    public bool IsSuccess { get; internal set; }
    public bool IsFailure => !IsSuccess;
    public List<Message>? Messages { get; internal set; }

    public Result(bool isSuccess, List<Message>? messages = null)
    {
        IsSuccess = isSuccess;

        if (messages is not null)
        {
            Messages = messages;
        }
    }

    public void AddMessage(Message Message)
    {
        Messages ??= [];
        Messages.Add(Message);
    }
}

public class Result<T> : Result
{
    public T Data { get; }

    public Result(T data, bool isSuccess, List<Message>? Messages = null) : base(isSuccess, Messages)
    {
        Data = data;
    }
}