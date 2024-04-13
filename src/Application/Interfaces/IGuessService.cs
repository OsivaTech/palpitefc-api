using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IGuessService
{
    Task<Result<GuessResponse>> Create(GuessRequest request, CancellationToken cancellationToken);
}