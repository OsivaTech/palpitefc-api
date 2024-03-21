using PalpiteFC.Api.Domain.Entities.Database;

namespace PalpiteFC.Api.Domain.Interfaces.Database;

public interface IOptionsRepository : IBaseRepository<Options>
{
    Task<int> InsertAndGetId(Options entity);
    Task<IEnumerable<Options>> SelectByVoteId(int voteId);
}
