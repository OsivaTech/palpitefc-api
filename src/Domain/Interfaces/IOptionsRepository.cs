using PalpiteApi.Domain.Entities;

namespace PalpiteApi.Domain.Interfaces;

public interface IOptionsRepository : IBaseRepository<Options> 
{
    Task<int> InsertAndGetId(Options entity);
    Task<IEnumerable<Options>> SelectByVoteId(int voteId);
}
