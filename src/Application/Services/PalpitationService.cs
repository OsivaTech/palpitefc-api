using Mapster;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Entities.Database;
using PalpiteApi.Domain.Errors;
using PalpiteApi.Domain.Interfaces.Database;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Services;
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
