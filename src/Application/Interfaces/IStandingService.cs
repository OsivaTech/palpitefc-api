using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IStandingService
{
    Task<Result<StandingResponse>> CreateOrUpdateAsync(StandingRequest request, CancellationToken cancellationToken);
    Task<Result<IEnumerable<StandingResponse>>> GetAsync(CancellationToken cancellationToken);
}
