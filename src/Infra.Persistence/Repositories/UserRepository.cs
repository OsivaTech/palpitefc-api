using Dapper;
using PalpiteApi.Domain.Entities.Database;
using PalpiteApi.Domain.Interfaces.Database;
using PalpiteApi.Infra.Persistence.Connection;

namespace PalpiteApi.Infra.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    #region Fields

    private readonly DbSession _session;

    #endregion

    #region Constructors

    public UserRepository(DbSession session)
    {
        _session = session;
    }

    #endregion

    #region Public Methods

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task Insert(Users entity)
    {
        await _session.Connection.ExecuteAsync("INSERT INTO users (name, email, password, role, userGuid) VALUES (@name, @email, @password, @role, @userGuid)",
                           new { entity.Name, entity.Email, entity.Password, entity.Role, entity.UserGuid }, _session.Transaction);
    }

    public async Task<IEnumerable<Users>> Select()
        => await _session.Connection.QueryAsync<Users>("SELECT * FROM users", null, _session.Transaction);
    public async Task<IEnumerable<Users>> SelectByPoints()
    => await _session.Connection.QueryAsync<Users>("SELECT * FROM users where points >= 1 order by points desc", null, _session.Transaction);

    public async Task<Users> Select(int id)
        => await _session.Connection.QuerySingleAsync<Users>("SELECT * FROM users WHERE id = @id", new { id }, _session.Transaction);

    public async Task<int> Exists(string email)
        => await _session.Connection.ExecuteAsync("SELECT * FROM users where email = @email", new { email }, _session.Transaction);

    public async Task<IEnumerable<Users>> FindByEmail(string email)
        => await _session.Connection.QueryAsync<Users>("SELECT * FROM users where email = @email", new { email }, _session.Transaction);

    public async Task Update(Users entity)
    {
        var query = @"UPDATE users 
                        SET name = @name, role = @role, points = @points, document = @document, team = @team, info = @info, number = @number, birthday = @birthday  
                      WHERE id = @id;";

        await _session.Connection.ExecuteAsync(query, new { entity.Name, entity.Role, entity.Points, entity.Document, entity.Team, entity.Info, entity.Number, entity.Birthday, entity.Id}, _session.Transaction);
    }
    public Task Update(int id) => throw new NotImplementedException();

    #endregion
}
