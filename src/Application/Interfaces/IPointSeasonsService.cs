using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IPointSeasonsService
{
    Task<Result<IEnumerable<PointSeasonsResponse>>> GetAsync(CancellationToken cancellationToken);
    Task<Result<PointSeasonsResponse>> GetCurrentAsync(CancellationToken cancellationToken);
    Task<Result<PointSeasonsResponse>> CreateAsync(PointSeasonsRequest request, CancellationToken cancellationToken);
    Task<Result<PointSeasonsResponse>> UpdateAsync(PointSeasonsRequest request, CancellationToken cancellationToken);
    Task<Result> DeteleAsync(int id, CancellationToken cancellationToken);
}
