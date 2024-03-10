using Dapper;
using PalpiteApi.Domain.Entities;
using PalpiteApi.Domain.Interfaces;
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

    public Task Delete(int id) => throw new NotImplementedException();

    public Task Insert(Votes obj) => throw new NotImplementedException();

    public async Task<IEnumerable<Votes>> Select()
        => await _session.Connection.QueryAsync<Votes>("SELECT * FROM votes", null, _session.Transaction);

    public async Task<Votes> Select(int id)
    {
        return await _session.Connection.QueryFirstOrDefaultAsync<Votes>("SELECT * FROM votes WHERE id = @id", new { id }, _session.Transaction);
    }

    public Task Update(Votes obj) => throw new NotImplementedException();
    public Task Update(int id) => throw new NotImplementedException();


    #endregion
}
