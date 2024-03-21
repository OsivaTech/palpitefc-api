using Dapper;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Persistence.Connection;

namespace PalpiteFC.Api.Persistence.Repositories;

public class NewsRepository : INewsRepository
{
    #region Fields

    private readonly DbSession _session;

    #endregion

    #region Constructors

    public NewsRepository(DbSession session)
    {
        _session = session;
    }

    #endregion

    #region Public Methods

    public async Task Delete(int id)
        => await _session.Connection.ExecuteAsync("DELETE FROM news WHERE id = @id", new { id }, _session.Transaction);

    public Task Insert(News entity)
    {
        throw new NotImplementedException();
    }

    public async Task<int> InsertAndGetId(News news)
    {
        var query = @"INSERT INTO news (title, content, info, userId, createdAt, updatedAt) VALUES(@title, @content, @info, @userId, current_timestamp(3), current_timestamp(3));
                      SELECT LAST_INSERT_ID() as id;";

        return await _session.Connection.QueryFirstAsync<int>(query, new { news.Title, news.Content, news.Info, news.UserId });
    }

    public async Task<IEnumerable<News>> Select()
        => await _session.Connection.QueryAsync<News>("SELECT * FROM news", null, _session.Transaction);

    public async Task<News> Select(int id)
        => await _session.Connection.QuerySingleAsync<News>("SELECT * FROM news WHERE id = @id", new { id }, _session.Transaction);

    public async Task Update(News entity)
    {
        var query = "UPDATE news SET title = @title, content = @content, info = @info, userId = @userId, updatedAt = current_timestamp(3) WHERE id = @id";

        await _session.Connection.ExecuteAsync(query, new { entity.Title, entity.Content, entity.Info, entity.UserId, entity.Id });
    }

    #endregion
}
