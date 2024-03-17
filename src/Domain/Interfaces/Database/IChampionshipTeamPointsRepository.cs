using PalpiteApi.Domain.Entities.Database;

namespace PalpiteApi.Domain.Interfaces.Database;

public interface IChampionshipTeamPointsRepository : IBaseRepository<ChampionshipTeamPoints>
{
    Task<int> InsertAndGetId(ChampionshipTeamPoints entity);
}
