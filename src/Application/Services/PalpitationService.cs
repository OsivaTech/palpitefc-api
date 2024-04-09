using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.CrossCutting.Errors;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;
public class PalpitationService : IPalpitationService
{
    private readonly IGuessesRepository _repository;
    private readonly UserContext _userContext;

    public PalpitationService(IGuessesRepository repository, UserContext userContext)
    {
        _repository = repository;
        _userContext = userContext;
    }

    public async Task<Result<PalpitationResponse>> Create(PalpitationRequest request, CancellationToken cancellationToken)
    {
        var palpitations = await _repository.SelectByUserIdAndGameId(_userContext.Id, request.GameId);

        if (palpitations.Any())
        {
            return ResultHelper.Failure<PalpitationResponse>(PalpitationErrors.PalpitationAlreadyExists);
        }

        var entity = request.Adapt<Guess>();
        entity.UserId = _userContext.Id;

        var id = await _repository.InsertAndGetId(entity);

        var result = await _repository.Select(id);

        return ResultHelper.Success(result.Adapt<PalpitationResponse>());
    }
}
