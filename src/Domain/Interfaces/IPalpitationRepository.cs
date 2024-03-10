using PalpiteApi.Domain.Entities;

namespace PalpiteApi.Domain.Interfaces;

public interface IPalpitationRepository : IBaseRepository<Palpitations>
{
    Task<IEnumerable<Palpitations>> SelectByUserIdAndGameId(int userId, int gameId);
}
