using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Interfaces.Database;

namespace PalpiteFC.Api.Domain.Interfaces;

public interface IPointSeasonsRepository : IBaseRepository<PointSeasons>
{
    Task<int> InsertAndGetId(PointSeasons entity);
    Task<PointSeasons> SelectCurrent();
}
