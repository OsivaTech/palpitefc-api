using PalpiteApi.Domain.Entities;

namespace PalpiteApi.Domain.Interfaces;

public interface IVotesRepository : IBaseRepository<Votes>
{
    Task<int> InsertAndGetId(Votes entity);
}
