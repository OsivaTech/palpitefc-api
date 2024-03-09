using Mapster;
using PalpiteApi.Application.Responses;
using PalpiteApi.Application.Services.Interfaces;
using PalpiteApi.Domain.Interfaces;

namespace PalpiteApi.Application.Services;

public class ChampionshipsService : IChampionshipsService
{
    #region Fields

    private readonly IChampionshipsRepository _champsRepository;

    #endregion

    #region Constructor

    public ChampionshipsService(IChampionshipsRepository rerpository)
    {
        _champsRepository = rerpository;
    }

    #endregion

    #region Public Methods

    public async Task<IEnumerable<ChampionshipResponse>> GetAsync(CancellationToken cancellationToken)
    {
        var championships = await _champsRepository.Select();

        return championships.Adapt<IEnumerable<ChampionshipResponse>>();
    }

    #endregion
}
