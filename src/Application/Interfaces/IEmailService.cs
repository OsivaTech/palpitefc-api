using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IEmailService
{
    Task<Result<string>> SendEmailCodeAsync(SendEmailCodeRequest request, CancellationToken cancellationToken);
}
