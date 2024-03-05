using Dapper;
using PalpiteApi.Domain.Entities;
using PalpiteApi.Domain.Interfaces;
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

    public async Task Insert(Users user)
    {
        await _session.Connection.ExecuteAsync("INSERT INTO users (name, email, password, role, userGuid) VALUES (@name, @email, @password, @role, @userGuid)",
                           new { user.Name, user.Email, user.Password, user.Role, user.UserGuid }, _session.Transaction);
    }

    public async Task<IEnumerable<Users>> Select()
        => await _session.Connection.QueryAsync<Users>("SELECT * FROM users", null, _session.Transaction);

    public Task<Users> Select(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Exists(string email)
        => await _session.Connection.ExecuteAsync("SELECT * FROM users where email = @email", new { email }, _session.Transaction);
    
    public async Task<IEnumerable<Users>> FindByEmail(string email)
        => await _session.Connection.QueryAsync<Users>("SELECT * FROM users where email = @email", new { email }, _session.Transaction);

    public Task Update(Users obj)
    {
        throw new NotImplementedException();
    }

    #endregion
}
