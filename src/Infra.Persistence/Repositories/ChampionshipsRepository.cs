using Dapper;
using PalpiteApi.Domain.Entities;
using PalpiteApi.Domain.Interfaces;
using PalpiteApi.Infra.Persistence.Connection;

namespace PalpiteApi.Infra.Persistence.Repositories;

public class ChampionshipsRepository : IChampionshipsRepository
{
    #region Fields

    private readonly DbSession _session;

    #endregion

    #region Constructors

    public ChampionshipsRepository(DbSession session)
    {
        _session = session;
    }

    #endregion

    #region Public Methods

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Insert(Championships obj)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Championships>> Select() 
        => await _session.Connection.QueryAsync<Championships>("SELECT * FROM championships", null, _session.Transaction);

    public Task<Championships> Select(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Championships obj)
    {
        throw new NotImplementedException();
    }
    public Task Update(int id) => throw new NotImplementedException();

    #endregion
}
