using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Api.Integrations.PagBank.Responses;

namespace PalpiteFC.Api.Integrations.Extensions;

public static class PagBankErrorResponseExtensions
{
    public static Result<T> ToResult<T>(this ErrorResponse response)
    {
        return ResultHelper.Failure<T>(response.ErrorMessages?.Select(r => new Message(r.Error ?? "Undefined Error", r.Description ?? "Unefined description")).ToList() ?? []);
    }
}