using Dapper;
using PalpiteApi.Domain.Entities.Database;
using PalpiteApi.Domain.Interfaces.Database;
using PalpiteApi.Infra.Persistence.Connection;

namespace PalpiteApi.Infra.Persistence.Repositories;

public class GamesRepository : IGamesRepository
{
    #region Fields

    private readonly DbSession _session;

    #endregion

    #region Constructors

    public GamesRepository(DbSession session)
    {
        _session = session;
    }

    #endregion

    #region Public Methods

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Insert(Games obj)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Games>> Select() 
        => await _session.Connection.QueryAsync<Games>("SELECT * FROM games", null, _session.Transaction);

    public Task<Games> Select(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Games obj)
    {
        throw new NotImplementedException();
    }
    public Task Update(int id) => throw new NotImplementedException();

    #endregion
}
