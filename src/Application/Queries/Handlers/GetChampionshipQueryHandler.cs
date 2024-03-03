using Mapster;
using MediatR;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Interfaces;

namespace PalpiteApi.Application.Queries.Handlers;

public class GetChampionshipQueryHandler : IRequestHandler<GetChampionshipQuery, IEnumerable<ChampionshipResponse>>
{
    #region Fields

    private readonly IChampionshipsRepository _champsRepository;
    private readonly IGamesRepository _gamesRepository;

    #endregion

    #region Constructor

    public GetChampionshipQueryHandler(IChampionshipsRepository rerpository, IGamesRepository gamesRepository)
    {
        _champsRepository = rerpository;
        _gamesRepository = gamesRepository;
    }

    #endregion

    #region Public Methods

    public async Task<IEnumerable<ChampionshipResponse>> Handle(GetChampionshipQuery request, CancellationToken cancellationToken)
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
