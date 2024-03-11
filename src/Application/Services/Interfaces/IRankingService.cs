
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Services.Interfaces;

public interface IRankingService
{
    Task<Result<IEnumerable<UserResponse>>> GetAsync(CancellationToken cancellationToken);
}