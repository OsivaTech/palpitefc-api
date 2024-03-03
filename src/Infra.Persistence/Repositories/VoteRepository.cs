using Dapper;
using PalpiteApi.Domain.Entities;
using PalpiteApi.Domain.Interfaces;
using PalpiteApi.Infra.Persistence.Connection;

namespace PalpiteApi.Infra.Persistence.Repositories;

public class VoteRepository : IVoteRepository
{
    #region Fields

    private readonly DbSession _session;

    #endregion

    #region Constructors

    public VoteRepository(DbSession session)
    {
        _session = session;
    }

    #endregion

    #region Pubic Methods

    public Task Delete(int id) => throw new NotImplementedException();

    public Task Insert(Votes obj) => throw new NotImplementedException();

    public async Task<IEnumerable<Votes>> Select()
        => await _session.Connection.QueryAsync<Votes>("SELECT * FROM votes", null, _session.Transaction);

    public Task<Votes> Select(int id) => throw new NotImplementedException();

    public Task Update(Votes obj) => throw new NotImplementedException();

    public async Task<IEnumerable<Votes>> SelectWithOptions()
    {
        string query = "SELECT * FROM votes INNER JOIN options ON votes.Id = options.voteId;";

        var votes = await _session.Connection.QueryAsync<Votes, Options, Votes>(
            query,
            (vote, option) =>
            {
                vote.Options ??= new List<Options>();
                vote.Options = vote.Options.Append(option);

                return vote;
            },
        splitOn: "Id",
        transaction: _session.Transaction
        );

        var result = votes.GroupBy(p => p.Id).Select(g =>
        {
            var groupedVotes = g.First();
            groupedVotes.Options = g.Select(p => p.Options!.Single()).ToList();

            return groupedVotes;
        });

        return result;
    }

    #endregion
}
