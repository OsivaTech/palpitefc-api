﻿using PalpiteApi.Domain.Result;

namespace PalpiteApi.Api.Extensions;

public static class ResultExtension
{
    public static IResult ToIResult<T>(this Result<T> result, int successCode = 200, int failureCode = 400)
    {
        if (result.IsSuccess)
        {
            return Results.Json(result.Data, statusCode: successCode);
        }

        return Results.Json(new { message = result.Messages!.First().Description }, statusCode: failureCode);
    }

    public static IResult ToIResult(this Result result, int successCode = 204, int failureCode = 400)
    {
        if (result.IsSuccess)
        {
            return Results.StatusCode(successCode);
        }

        return Results.Json(new { message = result.Messages!.First().Description }, statusCode: failureCode);
    }
}