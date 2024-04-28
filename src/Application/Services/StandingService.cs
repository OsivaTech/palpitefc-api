using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class StandingService : IStandingService
{
    private readonly IStandingsRepository _repository;

    public StandingService(IStandingsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<StandingResponse>>> GetAsync(CancellationToken cancellationToken)
    {
        var standings = await _repository.Select();

        return ResultHelper.Success(standings.Adapt<IEnumerable<StandingResponse>>());
    }

    public async Task<Result<StandingResponse>> CreateOrUpdateAsync(StandingRequest request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        if (id > 0)
        {
            await _repository.Update(request.Adapt<Standing>());
        }
        else
        {
            id = await _repository.InsertAndGetId(request.Adapt<Standing>());
        }

        var teamsPoints = await _repository.Select(id);

        return ResultHelper.Success(teamsPoints.Adapt<StandingResponse>());
    }
}
