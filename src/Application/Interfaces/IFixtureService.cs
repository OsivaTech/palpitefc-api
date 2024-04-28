using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IFixtureService
{
    Task<Result<FixtureResponse>> CreateOrUpdateAsync(FixtureRequest request, CancellationToken cancellationToken);
    Task<Result<FixtureResponse>> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<FixtureResponse>>> GetAsync(CancellationToken cancellationToken);
}