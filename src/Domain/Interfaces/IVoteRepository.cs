using PalpiteApi.Domain.Entities;

namespace PalpiteApi.Domain.Interfaces;

public interface IVoteRepository : IBaseRepository<Votes>
{
    Task<IEnumerable<Votes>> SelectWithOptions();
}
