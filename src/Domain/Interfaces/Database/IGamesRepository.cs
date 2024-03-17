using PalpiteApi.Domain.Entities.Database;

namespace PalpiteApi.Domain.Interfaces.Database;

public interface IGamesRepository : IBaseRepository<Games>
{
    Task<int> InsertAndGetId(Games entity);
}
