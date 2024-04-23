﻿using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class LeagueService : ILeagueService
{
    #region Fields

    private readonly ILeaguesRepository _repository;

    #endregion

    #region Constructor

    public LeagueService(ILeaguesRepository rerpository)
    {
        _repository = rerpository;
    }

    #endregion

    #region Public Methods

    public async Task<Result<IEnumerable<LeagueResponse>>> GetAsync(CancellationToken cancellationToken)
    {
        var championships = await _repository.Select();

        return ResultHelper.Success(championships.Adapt<IEnumerable<LeagueResponse>>());
    }

    public async Task<Result<LeagueResponse>> CreateOrUpdateAsync(LeagueRequest request, CancellationToken cancellationToken)
    {
        var id = request.Championship!.Id;

        if (request.Championship!.Id > 0)
        {
            await _repository.Update(request.Championship.Adapt<League>());
        }
        else
        {
            id = await _repository.InsertAndGetId(request.Championship.Adapt<League>());
        }

        var championship = await _repository.Select(id);

        return ResultHelper.Success(championship.Adapt<LeagueResponse>());
    }

    public async Task<Result<LeagueResponse>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _repository.Delete(id);

        return ResultHelper.Success<LeagueResponse>(new() { Id = id });
    }

    #endregion
}