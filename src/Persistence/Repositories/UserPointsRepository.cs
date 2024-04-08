using Dapper;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Persistence.Connection;

namespace PalpiteFC.Api.Persistence.Repositories;

public class UserPointsRepository : IUserPointsRepository
{
    #region Fields

    private readonly DbSession _session;

    #endregion

    #region Constructors

    public UserPointsRepository(DbSession session)
    {
        _session = session;
    }

    #endregion

    #region Public Methods

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Insert(UserPoints entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserPoints>> Select()
        => await _session.Connection.QueryAsync<UserPoints>("SELECT * FROM userPoints", null, _session.Transaction);

    public Task<UserPoints> Select(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserPoints>> SelectByPointSeasonId(int pointSeasonId)
    {
        var query = @"SELECT up.id
	                      ,up.points
	                      ,up.pointSeasonId
	                      ,up.gameId
	                      ,up.userId
	                      ,up.createdAt
	                      ,up.updatedAt
	                      ,u.*
                      FROM userPoints up
                      INNER JOIN users u ON up.userId = u.Id
                      WHERE pointSeasonId = @pointSeasonId;";

        return await _session.Connection.QueryAsync<UserPoints, Users, UserPoints>(query, (userPoints, users) =>
        {
            userPoints.User = users;
            return userPoints;
        }, new { pointSeasonId }, _session.Transaction, splitOn: "userId");
    }

    public Task Update(UserPoints entity)
    {
        throw new NotImplementedException();
    }

    #endregion
}
