using PalpiteApi.Domain.Entities;

namespace PalpiteApi.Domain.Interfaces;

public interface IOptionsRepository : IBaseRepository<Options> 
{
    Task<int> AddVote(int id, Options options);
    Task<IEnumerable<Options>> SelectByVoteId(int voteId);
}
