using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IFixtureService
{
    Task<Result<IEnumerable<FixtureResponse>>> GetAsync(CancellationToken cancellationToken);
}