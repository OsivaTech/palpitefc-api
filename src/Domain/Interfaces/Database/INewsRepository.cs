using PalpiteFC.Api.Domain.Entities.Database;

namespace PalpiteFC.Api.Domain.Interfaces.Database;

public interface INewsRepository : IBaseRepository<News>
{
    Task<int> InsertAndGetId(News news);
}
