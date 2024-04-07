using Dapper;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Interfaces;
using PalpiteFC.Api.Persistence.Connection;

namespace PalpiteFC.Api.Persistence.Repositories;

public class PointSeasonsRepository : IPointSeasonsRepository
{
    #region Fields

    private readonly DbSession _session;

    #endregion

    #region Constructors

    public PointSeasonsRepository(DbSession session)
    {
        _session = session;
    }

    #endregion

    #region Public Methods

    public async Task Delete(int id)
        => await _session.Connection.ExecuteAsync("DELETE FROM pointSeasons WHERE id = @id", new { id }, _session.Transaction);

    public async Task Insert(PointSeasons entity)
    {
        var query = @"INSERT INTO pointSeasons
                        (startDate, endDate, createdAt, updatedAt)
                        VALUES(@startDate, @endDate, current_timestamp(3), current_timestamp(3));";

        await _session.Connection.ExecuteAsync(query, entity, _session.Transaction);
    }

    public async Task<int> InsertAndGetId(PointSeasons entity)
    {
        var query = @"INSERT INTO pointSeasons
                        (startDate, endDate, createdAt, updatedAt)
                        VALUES(@startDate, @endDate, current_timestamp(3), current_timestamp(3));
                      SELECT LAST_INSERT_ID() as id;";

        return await _session.Connection.QueryFirstAsync<int>(query, entity, _session.Transaction);
    }

    public async Task<IEnumerable<PointSeasons>> Select()
        => await _session.Connection.QueryAsync<PointSeasons>("SELECT * FROM pointSeasons", null, _session.Transaction);

    public async Task<PointSeasons> Select(int id)
        => (await _session.Connection.QueryFirstOrDefaultAsync<PointSeasons>("SELECT * FROM pointSeasons WHERE id = @id", new { id }, _session.Transaction))!;

    public async Task<PointSeasons> SelectCurrent()
    {
        var query = "SELECT * FROM pointSeasons WHERE current_timestamp(3) BETWEEN startDate AND endDate";

        return (await _session.Connection.QueryFirstOrDefaultAsync<PointSeasons>(query, null, _session.Transaction))!;
    }

    public async Task Update(PointSeasons entity)
    {
        var query = "UPDATE pointSeasons SET startDate = @startDate, endDate = @endDate, updatedAt = current_timestamp(3) WHERE id = @id";

        await _session.Connection.ExecuteAsync(query, entity, _session.Transaction);
    }

    #endregion
}
