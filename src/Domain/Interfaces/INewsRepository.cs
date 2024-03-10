using PalpiteApi.Domain.Entities;

namespace PalpiteApi.Domain.Interfaces;

public interface INewsRepository : IBaseRepository<News>
{
    Task<int> InsertAndGetId(News news);
}
