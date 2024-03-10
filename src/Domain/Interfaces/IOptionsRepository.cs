using PalpiteApi.Domain.Entities;

namespace PalpiteApi.Domain.Interfaces;

public interface IOptionsRepository : IBaseRepository<Options> 
{
    Task<int> AddVote(Options entity, int count);
    Task<IEnumerable<Options>> SelectByVoteId(int voteId);
}
