using PalpiteApi.Domain.Entities.Database;

namespace PalpiteApi.Domain.Interfaces.Database;

public interface IChampionshipsRepository : IBaseRepository<Championships>
{
    Task<int> InsertAndGetId(Championships entity);
}

