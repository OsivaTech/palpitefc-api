using Dapper;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Persistence.Connection;

namespace PalpiteFC.Api.Persistence.Repositories;

public class TeamsGameRepository : ITeamsGamesRepository
{
    #region Fields

    private readonly DbSession _session;

    #endregion

    #region Constructors

    public TeamsGameRepository(DbSession session)
    {
        _session = session;
    }

    #endregion

    #region Public Methods

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Insert(TeamsGame obj)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TeamsGame>> Select()
        => await _session.Connection.QueryAsync<TeamsGame>("SELECT * FROM teamsGame", null, _session.Transaction);

    public Task<TeamsGame> Select(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(TeamsGame obj)
    {
        throw new NotImplementedException();
    }
    public Task Update(int id) => throw new NotImplementedException();
    #endregion
}
