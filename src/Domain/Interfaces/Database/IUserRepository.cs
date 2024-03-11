using PalpiteApi.Domain.Entities.Database;

namespace PalpiteApi.Domain.Interfaces.Database;

public interface IUserRepository : IBaseRepository<Users>
{
    Task<int> Exists(string email);
    Task<IEnumerable<Users>> FindByEmail(string email);
    Task<IEnumerable<Users>> SelectByPoints();
}
