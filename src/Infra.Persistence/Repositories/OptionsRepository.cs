using Dapper;
using PalpiteApi.Domain.Entities;
using PalpiteApi.Domain.Interfaces;
using PalpiteApi.Infra.Persistence.Connection;

namespace PalpiteApi.Infra.Persistence.Repositories;
public class OptionsRepository : IOptionsRepository
{
    #region Fields

    private readonly DbSession _session;

    #endregion

    #region Constructors

    public OptionsRepository(DbSession session)
    {
        _session = session;
    }

    #endregion

    #region Public Methods

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Insert(Options obj)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Options>> Select()
        => await _session.Connection.QueryAsync<Options>("SELECT * FROM options", null, _session.Transaction);

    public Task<Options> Select(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Options obj)
    {
        throw new NotImplementedException();
    }

    #endregion
}
