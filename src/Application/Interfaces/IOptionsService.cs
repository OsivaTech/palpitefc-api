﻿using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IOptionsService
{
    Task<Result<VoteResponse>> ComputeVoteAsync(OptionsRequest request, CancellationToken cancellationToken);
    Task<Result<OptionsResponse>> CreateAsync(OptionsRequest request, CancellationToken cancellationToken);
}
