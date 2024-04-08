using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IRankingService
{
    Task<Result<IEnumerable<RankingResponse>>> GetAsync(CancellationToken cancellationToken);
}