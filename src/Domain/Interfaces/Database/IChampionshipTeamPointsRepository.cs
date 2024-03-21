using PalpiteFC.Api.Domain.Entities.Database;

namespace PalpiteFC.Api.Domain.Interfaces.Database;

public interface IChampionshipTeamPointsRepository : IBaseRepository<ChampionshipTeamPoints>
{
    Task<int> InsertAndGetId(ChampionshipTeamPoints entity);
}
