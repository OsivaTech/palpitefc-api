namespace PalpiteApi.Domain.Interfaces.Database;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void Commit();
    void Rollback();
}