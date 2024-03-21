using Dapper;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Persistence.Connection;

namespace PalpiteFC.Api.Persistence.Repositories;

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
        var query = @"SELECT * FROM palpitations
                        WHERE userId = @userId AND gameId = @gameId";

        return await _session.Connection.QueryAsync<Palpitations>(query, new { userId, gameId }, _session.Transaction);
    }

    public async Task Insert(Palpitations entity)
    {
        var query = @"INSERT INTO palpitations
                        (firstTeamId, firstTeamGol, secondTeamId, secondTeamGol, userId, gameId, createdAt, updatedAt)
                        VALUES(@firstTeamId, @firstTeamGol, @secondTeamId, @secondTeamGol, @userId, @gameId, current_timestamp(3), current_timestamp(3));";

        await _session.Connection.ExecuteAsync(query, entity, _session.Transaction);
    }

    public async Task<int> InsertAndGetId(Palpitations entity)
    {
        var query = @"INSERT INTO palpitations
                        (firstTeamId, firstTeamGol, secondTeamId, secondTeamGol, userId, gameId, createdAt, updatedAt)
                        VALUES(@firstTeamId, @firstTeamGol, @secondTeamId, @secondTeamGol, @userId, @gameId, current_timestamp(3), current_timestamp(3));
                      SELECT LAST_INSERT_ID() as id;";

        return await _session.Connection.QuerySingleAsync<int>(query, entity, _session.Transaction);
    }

    public Task<IEnumerable<Palpitations>> Select()
    {
        throw new NotImplementedException();
    }

    public async Task<Palpitations> Select(int id)
        => await _session.Connection.QuerySingleAsync<Palpitations>("SELECT * FROM palpitations WHERE id = @id", new { id }, _session.Transaction);

    public Task Update(Palpitations obj)
    {
        throw new NotImplementedException();
    }

    #endregion
}
