using PalpiteFC.Api.Domain.Entities.Database;

namespace PalpiteFC.Api.Domain.Interfaces.Database;

public interface IPalpitationRepository : IBaseRepository<Palpitations>
{
    Task<int> InsertAndGetId(Palpitations entity);
    Task<IEnumerable<Palpitations>> SelectByUserIdAndGameId(int userId, int gameId);
}
