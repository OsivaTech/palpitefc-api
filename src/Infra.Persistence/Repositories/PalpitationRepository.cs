using Dapper;
using PalpiteApi.Domain.Entities.Database;
using PalpiteApi.Domain.Interfaces.Database;
using PalpiteApi.Infra.Persistence.Connection;

namespace PalpiteApi.Infra.Persistence.Repositories;

public class PalpitationRepository : IPalpitationRepository
{
    #region Fields

    private readonly DbSession _session;

    #endregion

    #region Constructors

    public PalpitationRepository(DbSession session)
    {
        _session = session;
    }

    #endregion

    #region Public Methods

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Palpitations>> SelectByUserIdAndGameId(int userId, int gameId)
    {
        var query = @"SELECT * FROM PalpiteApi.palpitations
                        WHERE userId = @userId AND gameId = @gameId";

        return await _session.Connection.QueryAsync<Palpitations>(query, new { userId, gameId }, _session.Transaction);
    }

    public async Task Insert(Palpitations palpitation)
    {
        var query = @"INSERT INTO PalpiteApi.palpitations
                        (firstTeamId, firstTeamGol, secondTeamId, secondTeamGol, userId, gameId, createdAt, updatedAt)
                        VALUES(@firstTeamId, @firstTeamGol, @secondTeamId, @secondTeamGol, @userId, @gameId, current_timestamp(3), current_timestamp(3));";

        await _session.Connection.ExecuteAsync(query, palpitation, _session.Transaction);
    }

    public Task<IEnumerable<Palpitations>> Select()
    {
        throw new NotImplementedException();
    }

    public Task<Palpitations> Select(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Palpitations obj)
    {
        throw new NotImplementedException();
    }

    #endregion
}
