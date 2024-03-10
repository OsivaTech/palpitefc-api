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

    public Task Insert(Options entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Options>> Select()
        => await _session.Connection.QueryAsync<Options>("SELECT * FROM options", null, _session.Transaction);

    public async Task<Options> Select(int id)
    {
       return await _session.Connection.QueryFirstAsync<Options>("SELECT * FROM options WHERE id = @id", new { id }, _session.Transaction);
    }

    public Task Update(int count)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddVote(Options entity, int count)
    {
        return _session.Connection.ExecuteAsync("UPDATE options SET count = @count where id = @Id, updatedAt = current_timestamp(3)", new {count, entity.Id}, _session.Transaction);

    }
    public async Task<IEnumerable<Options>> SelectByVoteId(int voteId)
    {
        return await _session.Connection.QueryAsync<Options>("SELECT * FROM options WHERE voteId = @voteId", new { voteId }, _session.Transaction);
    }
    public Task Update(Options entity)
    {
        throw new NotImplementedException();
    }

    #endregion
}
