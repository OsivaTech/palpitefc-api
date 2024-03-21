using PalpiteFC.Api.Domain.Entities.Database;

namespace PalpiteFC.Api.Domain.Interfaces.Database;

public interface IVotesRepository : IBaseRepository<Votes>
{
    Task<int> InsertAndGetId(Votes entity);
}
