﻿using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Services;

public class ChampionshipsService : IChampionshipsService
{
    #region Fields

    private readonly IChampionshipsRepository _repository;

    #endregion

    #region Constructor

    public ChampionshipsService(IChampionshipsRepository rerpository)
    {
        _repository = rerpository;
    }

    #endregion

    #region Public Methods

    public async Task<Result<IEnumerable<ChampionshipResponse>>> GetAsync(CancellationToken cancellationToken)
    {
        var championships = await _repository.Select();

        return ResultHelper.Success(championships.Adapt<IEnumerable<ChampionshipResponse>>());
    }

    public async Task<Result<ChampionshipResponse>> CreateOrUpdateAsync(ChampionshipRequest request, CancellationToken cancellationToken)
    {
        var id = request.Championship!.Id;

        if (request.Championship!.Id > 0)
        {
            await _repository.Update(request.Championship.Adapt<Championships>());
        }
        else
        {
            id = await _repository.InsertAndGetId(request.Championship.Adapt<Championships>());
        }

        var championship = await _repository.Select(id);

        return ResultHelper.Success(championship.Adapt<ChampionshipResponse>());
    }

    public async Task<Result<ChampionshipResponse>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _repository.Delete(id);

        return ResultHelper.Success<ChampionshipResponse>(new() { Id = id });
    }

    #endregion
}
