using Mapster;
using PalpiteApi.Application.Responses;
using PalpiteApi.Application.Services.Interfaces;
using PalpiteApi.Domain.Interfaces;

namespace PalpiteApi.Application.Services;

public class ChampionshipsService : IChampionshipsService
{
    #region Fields

    private readonly IChampionshipsRepository _champsRepository;
    private readonly IGamesRepository _gamesRepository;

    #endregion

    #region Constructor

    public ChampionshipsService(IChampionshipsRepository rerpository, IGamesRepository gamesRepository)
    {
        _champsRepository = rerpository;
        _gamesRepository = gamesRepository;
    }

    #endregion

    #region Public Methods

    public async Task<IEnumerable<ChampionshipResponse>> GetAsync(CancellationToken cancellationToken)
    {
        var championships = await _champsRepository.Select();
        var games = await _gamesRepository.Select();

        var champsResponse = new List<ChampionshipResponse>();

        foreach (var championship in championships)
        {
            champsResponse.Add((games.Where(w => w.ChampionshipId == championship.Id), championship).Adapt<ChampionshipResponse>());
        }

        return champsResponse;
    }

    #endregion
}
