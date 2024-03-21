namespace PalpiteFC.Api.Domain.Interfaces.Database;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void Commit();
    void Rollback();
}