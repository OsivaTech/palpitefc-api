using PalpiteApi.Domain.Entities.Database;

namespace PalpiteApi.Domain.Interfaces.Database;

public interface IVotesRepository : IBaseRepository<Votes>
{
    Task<int> InsertAndGetId(Votes entity);
}
