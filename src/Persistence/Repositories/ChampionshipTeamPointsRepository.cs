using Dapper;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Persistence.Connection;
using static Dapper.SqlMapper;

namespace PalpiteFC.Api.Persistence.Repositories;

public class ChampionshipTeamPointsRepository : IChampionshipTeamPointsRepository
{
    #region Fields

    private readonly DbSession _session;

    #endregion

    #region Constructors

    public ChampionshipTeamPointsRepository(DbSession session)
    {
        _session = session;
    }

    #endregion

    #region Public Methods

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Insert(ChampionshipTeamPoints entity)
    {
        throw new NotImplementedException();
    }

    public async Task<int> InsertAndGetId(ChampionshipTeamPoints entity)
    {
        var query = @"INSERT INTO championshipTeamPoints (position, points, teamId, championshipsId, createdAt, updatedAt) VALUES(@position, @points, @teamId, @championshipsId, current_timestamp(3), current_timestamp(3));
                      SELECT LAST_INSERT_ID() as id;"
        ;
        return await _session.Connection.QuerySingleAsync<int>(query, new { entity.Position, entity.Points, entity.TeamId, entity.ChampionshipsId }, _session.Transaction);
    }
    public async Task<IEnumerable<ChampionshipTeamPoints>> Select()
    => await _session.Connection.QueryAsync<ChampionshipTeamPoints>("SELECT * FROM championshipTeamPoints", null, _session.Transaction);

    public async Task<ChampionshipTeamPoints> Select(int id)
        => await _session.Connection.QuerySingleAsync<ChampionshipTeamPoints>("SELECT * FROM championshipTeamPoints WHERE id = @id", new { id }, _session.Transaction);

    public async Task Update(ChampionshipTeamPoints entity)
        => await _session.Connection.ExecuteAsync("UPDATE championshipTeamPoints SET position = @position, points = @points, teamId = @teamId, championshipsId = @championshipsId, updatedAt = current_timestamp(3) WHERE id = @id",
            new { entity.Position, entity.Points, entity.TeamId, entity.ChampionshipsId, entity.Id }, _session.Transaction);

    #endregion
}
