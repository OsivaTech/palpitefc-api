using Mapster;
using PalpiteApi.Application.Requests;
using PalpiteApi.Domain.Entities.Database;
using PalpiteApi.Domain.Errors;
using PalpiteApi.Domain.Interfaces.Database;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Services.Auth;
public class PalpitationService : IPalpitationService
{
    private readonly IPalpitationRepository _repository;
    private readonly UserContext _userContext;

    public PalpitationService(IPalpitationRepository repository, UserContext userContext)
    {
        _repository = repository;
        _userContext = userContext;
    }

    public async Task<Result> Create(PalpitationRequest request, CancellationToken cancellationToken)
    {
        var palpitations = await _repository.SelectByUserIdAndGameId(_userContext.Id, request.GameId);

        if (palpitations.Any())
        {
            return ResultHelper.Failure(PalpitationErrors.PalpitationAlreadyExists);
        }

        var palpitationEntity = request.Adapt<Palpitations>();

        palpitationEntity.UserId = _userContext.Id;

        await _repository.Insert(palpitationEntity);

        return ResultHelper.Success();
    }
}
