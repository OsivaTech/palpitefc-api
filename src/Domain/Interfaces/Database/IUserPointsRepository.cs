using PalpiteFC.Api.Domain.Entities.Database;

namespace PalpiteFC.Api.Domain.Interfaces.Database;

public interface IUserPointsRepository : IBaseRepository<UserPoints>
{
    Task<IEnumerable<UserPoints>> SelectByPointSeasonId(int pointSeasonId);
}
