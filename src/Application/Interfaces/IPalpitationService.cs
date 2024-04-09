﻿using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IPalpitationService
{
    Task<Result<PalpitationResponse>> Create(PalpitationRequest request, CancellationToken cancellationToken);
}