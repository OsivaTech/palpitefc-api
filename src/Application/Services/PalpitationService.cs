using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Errors;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Services;
public class PalpitationService : IPalpitationService
{
    private readonly IPalpitationRepository _repository;
    private readonly UserContext _userContext;

    public PalpitationService(IPalpitationRepository repository, UserContext userContext)
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

        var entity = request.Adapt<Palpitations>();
        entity.UserId = _userContext.Id;

        var id = await _repository.InsertAndGetId(entity);

        var result = await _repository.Select(id);

        return ResultHelper.Success(result.Adapt<PalpitationResponse>());
    }
}
