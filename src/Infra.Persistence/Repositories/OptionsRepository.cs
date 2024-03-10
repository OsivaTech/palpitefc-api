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

    public async Task<Options> Select(int id)
    {
       return await _session.Connection.QueryFirstAsync<Options>("SELECT * FROM options WHERE id = @id", new { id }, _session.Transaction);
    }

    public Task Update(int count)
    {
        return _session.Connection.ExecuteAsync("UPDATE options SET count = @count", count, _session.Transaction);
    }

    public Task<int> AddVote(int count, Options obj)
    {
        return _session.Connection.ExecuteAsync("UPDATE options SET count = @count where id = @Id", new {count, obj.Id}, _session.Transaction);

    }
    public async Task<IEnumerable<Options>> SelectByVoteId(int voteId)
    {
        return await _session.Connection.QueryAsync<Options>("SELECT * FROM options WHERE voteId = @voteId", new { voteId }, _session.Transaction);
    }
    public Task Update(Options obj)
    {
        throw new NotImplementedException();
    }

    #endregion
}
