using PalpiteFC.Api.Domain.Entities.Database;

namespace PalpiteFC.Api.Domain.Interfaces.Database;

public interface IConfigRepository : IBaseRepository<Config>
{
    Task<IEnumerable<Config>> Select(string name);
}
