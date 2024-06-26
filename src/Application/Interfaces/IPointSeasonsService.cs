﻿using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IPointSeasonsService
{
    Task<Result<IEnumerable<PointSeasonsResponse>>> GetAsync(CancellationToken cancellationToken);
    Task<Result<PointSeasonsResponse>> GetCurrentAsync(CancellationToken cancellationToken);
    Task<Result<PointSeasonsResponse>> CreateAsync(PointSeasonsRequest request, CancellationToken cancellationToken);
    Task<Result<PointSeasonsResponse>> UpdateAsync(int id, PointSeasonsRequest request, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);
}
