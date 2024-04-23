using FluentValidation.Results;

namespace PalpiteFC.Api.Extensions;

public static class ValidationResultExtensions
{
    public static IResult ToIResult(this ValidationResult result, int statusCode = 400) 
        => Results.Json(new { Code = result.Errors.First().ErrorCode, Message = result.Errors.First().ErrorMessage }, statusCode: statusCode);
}
