using PalpiteApi.Domain.Entities.Database;

namespace PalpiteApi.Domain.Interfaces.Database;

public interface IConfigRepository : IBaseRepository<Config>
{
    Task<IEnumerable<Config>> Select(string name);
}
