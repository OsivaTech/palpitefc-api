using Dapper;
using PalpiteApi.Infra.Persistence.Connection;
using PalpiteApi.Infra.Persistence.Entities;

namespace PalpiteApi.Infra.Persistence.Repositories;

public class ChampionshipsRepository
{
    #region Fields

    private readonly DbSession _session;

    #endregion

    #region Constructors

    public ChampionshipsRepository(DbSession session)
    {
        _session = session;
    }

    #endregion

    #region Public Methods

    public IEnumerable<Championships> Get()
    {
        return _session.Connection.Query<Championships>("SELECT * FROM championships", null, _session.Transaction);
    }

    #endregion
}
