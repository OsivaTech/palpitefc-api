using Dapper;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Persistence.Connection;

namespace PalpiteFC.Api.Persistence.Repositories;

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

    public async Task Delete(int id)
        => await _session.Connection.ExecuteAsync("DELETE FROM games WHERE id = @id", new { id }, _session.Transaction);

    public Task Insert(Games entity)
        => throw new NotImplementedException();

    public async Task<int> InsertAndGetId(Games entity)
    {
        var query = @"INSERT INTO games (name, championshipId, start, createdAt, updatedAt) VALUES(@name, @championshipId, @start, current_timestamp(3), current_timestamp(3));
                      SELECT LAST_INSERT_ID() as id;";

        return await _session.Connection.QuerySingleAsync<int>(query, new { entity.Name, entity.ChampionshipId, entity.Start }, _session.Transaction);
    }

    public async Task<IEnumerable<Games>> Select()
        => await _session.Connection.QueryAsync<Games>("SELECT * FROM games", null, _session.Transaction);

    public async Task<Games> Select(int id)
        => await _session.Connection.QuerySingleAsync<Games>("SELECT * FROM games WHERE id = @id", new { id }, _session.Transaction);

    public async Task Update(Games entity)
        => await _session.Connection.ExecuteAsync("UPDATE games SET name = @name, championshipId = @championshipId, start = @start, finished = @finished, updatedAt = current_timestamp(3) WHERE id = @id",
            new { entity.Name, entity.ChampionshipId, entity.Start, entity.Finished, entity.Id }, _session.Transaction);

    public Task Update(int id) => throw new NotImplementedException();

    #endregion
}
