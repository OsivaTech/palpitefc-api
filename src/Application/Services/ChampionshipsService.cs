using Mapster;
using PalpiteApi.Application.Responses;
using PalpiteApi.Application.Services.Interfaces;
using PalpiteApi.Domain.Entities;
using PalpiteApi.Domain.Interfaces;

namespace PalpiteApi.Application.Services;

public class ChampionshipsService : IChampionshipsService
{
    #region Fields

    private readonly IChampionshipsRepository _repository;

    #endregion

    #region Constructor

    public ChampionshipsService(IChampionshipsRepository rerpository)
    {
        _repository = rerpository;
    }

    #endregion

    #region Public Methods

    public async Task<IEnumerable<ChampionshipResponse>> GetAsync(CancellationToken cancellationToken)
    {
        var championships = await _repository.Select();

        return championships.Adapt<IEnumerable<ChampionshipResponse>>();
    }

    public async Task<ChampionshipResponse> CreateOrUpdateAsync(ChampionshipRequest request, CancellationToken cancellationToken)
    {
        var championship = new Championships();

        if (request.Championship!.Id > 0)
        {
            await _repository.Update(request.Championship.Adapt<Championships>());
        }
        else
        {
            var id = await _repository.InsertAndGetId(request.Championship.Adapt<Championships>());

            championship = await _repository.Select(id);
        }

        return championship.Adapt<ChampionshipResponse>();
    }

    public async Task<ChampionshipResponse> DeleteAsync(int id)
    {
        await _repository.Delete(id);

        var championship = await _repository.Select();

        return championship.Adapt<ChampionshipResponse>();
    }

    #endregion
}
