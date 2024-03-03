namespace PalpiteApi.Infra.Persistence.Connection;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void Commit();
    void Rollback();
}