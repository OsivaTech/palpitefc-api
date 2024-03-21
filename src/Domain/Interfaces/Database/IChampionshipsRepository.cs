using PalpiteFC.Api.Domain.Entities.Database;

namespace PalpiteFC.Api.Domain.Interfaces.Database;

public interface IChampionshipsRepository : IBaseRepository<Championships>
{
    Task<int> InsertAndGetId(Championships entity);
}

