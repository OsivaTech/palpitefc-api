using PalpiteApi.Domain.Entities.Database;

namespace PalpiteApi.Domain.Interfaces.Database;

public interface IPalpitationRepository : IBaseRepository<Palpitations>
{
    Task<IEnumerable<Palpitations>> SelectByUserIdAndGameId(int userId, int gameId);
}
