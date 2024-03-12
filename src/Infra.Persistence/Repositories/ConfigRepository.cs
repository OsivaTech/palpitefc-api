using Dapper;
using PalpiteApi.Domain.Entities.Database;
using PalpiteApi.Domain.Interfaces.Database;
using PalpiteApi.Infra.Persistence.Connection;

namespace PalpiteApi.Infra.Persistence.Repositories;

public class ConfigRepository : IConfigRepository
{
    #region Fields

    private readonly DbSession _session;

    #endregion

    #region Constructors

    public ConfigRepository(DbSession session)
    {
        _session = session;
    }

    #endregion

    #region Public Methods

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task Insert(Config entity)
    {
        var query = "INSERT INTO config (name, value) VALUES(@name, @value);";

        await _session.Connection.ExecuteAsync(query, new { entity.Value, entity.Name }, _session.Transaction);
    }

    public Task<IEnumerable<Config>> Select()
    {
        throw new NotImplementedException();
    }

    public Task<Config> Select(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Config>> Select(string name)
    {
        var query = "SELECT * FROM config WHERE name = @name";

        return await _session.Connection.QueryAsync<Config>(query, new { name }, _session.Transaction);
    }

    public async Task Update(Config entity)
    {
        var query = "UPDATE config SET value = @value WHERE name = @name";

        await _session.Connection.ExecuteAsync(query, new { entity.Value, entity.Name }, _session.Transaction);
    }

    #endregion
}
