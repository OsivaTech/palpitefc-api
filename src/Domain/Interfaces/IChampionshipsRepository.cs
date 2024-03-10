using PalpiteApi.Domain.Entities;

namespace PalpiteApi.Domain.Interfaces;

public interface IChampionshipsRepository : IBaseRepository<Championships>
{
    Task<int> InsertAndGetId(Championships entity);
}

