using PalpiteApi.Domain.Entities;

namespace PalpiteApi.Domain.Interfaces;
public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> Select();
    Task<TEntity> Select(int id);
    Task Insert(TEntity obj);
    Task Update(TEntity obj);
    Task Delete(int id);
}