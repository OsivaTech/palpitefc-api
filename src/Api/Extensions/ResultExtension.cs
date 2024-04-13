using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Extensions;

public static class ResultExtension
{
    public static IResult ToIResult<T>(this Result<T> result, int successCode = 200, int failureCode = 400)
    {
        if (result.IsSuccess && result.Data is null)
        {
            return Results.Json(result.Data, statusCode: 204);
        }

        if (result.IsSuccess)
        {
            return Results.Json(result.Data, statusCode: successCode);
        }

        return Results.Json(CreateMessageObject(result.Messages), statusCode: failureCode);
    }

    public static IResult ToIResult(this Result result, int successCode = 204, int failureCode = 400)
    {
        if (result.IsSuccess)
        {
            return Results.StatusCode(successCode);
        }

        return Results.Json(CreateMessageObject(result.Messages), statusCode: failureCode);
    }

    private static object CreateMessageObject(List<Message>? messages)
    {
        var first = messages?.FirstOrDefault() ?? new Message("Unknown.Error", "Unknown error.");

        return new { code = first.Code, message = first.Description };
    }
}
