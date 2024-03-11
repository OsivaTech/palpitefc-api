using PalpiteApi.Domain.Entities.Database;

namespace PalpiteApi.Domain.Interfaces.Database;

public interface IOptionsRepository : IBaseRepository<Options>
{
    Task<int> InsertAndGetId(Options entity);
    Task<IEnumerable<Options>> SelectByVoteId(int voteId);
}
