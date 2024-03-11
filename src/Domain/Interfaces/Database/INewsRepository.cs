using PalpiteApi.Domain.Entities.Database;

namespace PalpiteApi.Domain.Interfaces.Database;

public interface INewsRepository : IBaseRepository<News>
{
    Task<int> InsertAndGetId(News news);
}
