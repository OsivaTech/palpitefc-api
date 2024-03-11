using PalpiteApi.Domain.Entities.Database;

namespace PalpiteApi.Domain.Interfaces.Database;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> Select();
    Task<TEntity> Select(int id);
    Task Insert(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(int id);
}