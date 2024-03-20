
using PalpiteApi.Application.Requests;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Interfaces;

public interface IEmailService
{
    Task<Result<string>> SendEmailCodeAsync(SendEmailCodeRequest request, CancellationToken cancellationToken);
}
