using PalpiteFC.Api.Domain.Entities.Database;

namespace PalpiteFC.Api.Domain.Interfaces.Database;

public interface IGamesRepository : IBaseRepository<Games>
{
    Task<int> InsertAndGetId(Games entity);
}
