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

    public async Task Insert(Options entity)
    {
        var query = "INSERT INTO options (title, count, voteId, createdAt, updatedAt) VALUES(@title, @count, @voteId, current_timestamp(3), current_timestamp(3))";

        await _session.Connection.ExecuteAsync(query, new { entity.Title, entity.Count, entity.VoteId });
    }

    public async Task<IEnumerable<Options>> Select()
        => await _session.Connection.QueryAsync<Options>("SELECT * FROM options", null, _session.Transaction);

    public async Task<Options> Select(int id)
    {
        return await _session.Connection.QuerySingleAsync<Options>("SELECT * FROM options WHERE id = @id", new { id }, _session.Transaction);
    }

    public Task Update(int count)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Options>> SelectByVoteId(int voteId)
        => await _session.Connection.QueryAsync<Options>("SELECT * FROM options WHERE voteId = @voteId", new { voteId }, _session.Transaction);

    public Task Update(Options entity)
        => _session.Connection.ExecuteAsync("UPDATE options SET title = @title, count = @count, voteId = @voteId, updatedAt = current_timestamp(3) WHERE id = @id", new { entity.Title, entity.Count, entity.VoteId, entity.Id });

    public async Task<int> InsertAndGetId(Options entity)
    {
        var query = @"INSERT INTO options (title, count, voteId, createdAt, updatedAt) VALUES(@title, @count, @voteId, current_timestamp(3), current_timestamp(3));
                      SELECT LAST_INSERT_ID() as id; ";

        return await _session.Connection.QuerySingleAsync<int>(query, new { entity.Title, entity.Count, entity.VoteId });
    }

    #endregion
}
