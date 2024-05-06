using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IPointsService
{
    Task<Result<IEnumerable<PointsResponse>>> GetCurrentAsync(CancellationToken cancellationToken);
}
