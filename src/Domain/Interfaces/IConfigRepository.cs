using PalpiteApi.Domain.Entities;

namespace PalpiteApi.Domain.Interfaces;

public interface IConfigRepository : IBaseRepository<Config>
{
    Task<IEnumerable<Config>> Select(string name);
}
