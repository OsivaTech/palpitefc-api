﻿using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface ITeamService
{
    Task<Result<IEnumerable<TeamResponse>>> GetAsync(CancellationToken cancellationToken);
}