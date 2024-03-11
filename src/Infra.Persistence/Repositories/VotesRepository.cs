using Dapper;
using PalpiteApi.Domain.Entities.Database;
using PalpiteApi.Domain.Interfaces.Database;
using PalpiteApi.Infra.Persistence.Connection;

namespace PalpiteApi.Infra.Persistence.Repositories;

public class VotesRepository : IVotesRepository
{
    #region Fields

    private readonly DbSession _session;

    #endregion

    #region Constructors

    public VotesRepository(DbSession session)
    {
        _session = session;
    }

    #endregion

    #region Pubic Methods

    public async Task Delete(int id)
        => await _session.Connection.ExecuteAsync("DELETE FROM votes WHERE id = @id", new { id }, _session.Transaction);

    public Task Insert(Votes entity) => throw new NotImplementedException();

    public async Task<int> InsertAndGetId(Votes entity)
    {
        var query = @"INSERT INTO votes (title, createdAt, updatedAt) VALUES(@title, current_timestamp(3), current_timestamp(3));
                      SELECT LAST_INSERT_ID() as id;";

        return await _session.Connection.QuerySingleAsync<int>(query, new { entity.Title }, _session.Transaction);
    }

    public async Task<IEnumerable<Votes>> Select()
        => await _session.Connection.QueryAsync<Votes>("SELECT * FROM votes", null, _session.Transaction);

    public async Task<Votes> Select(int id)
        => await _session.Connection.QuerySingleAsync<Votes>("SELECT * FROM votes WHERE id = @id", new { id }, _session.Transaction);

    public Task Update(Votes entity) => throw new NotImplementedException();

    #endregion
}
