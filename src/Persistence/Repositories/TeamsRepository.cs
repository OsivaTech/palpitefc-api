﻿using Dapper;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Persistence.Connection;

namespace PalpiteFC.Api.Persistence.Repositories;

public class TeamsRepository : ITeamsRepository
{
    #region Fields

    private readonly DbSession _session;

    #endregion

    #region Constructors

    public TeamsRepository(DbSession session)
    {
        _session = session;
    }

    #endregion

    #region Public Methods

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Insert(Teams obj)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Teams>> Select()
        => await _session.Connection.QueryAsync<Teams>("SELECT * FROM teams", null, _session.Transaction);

    public Task<Teams> Select(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Teams obj)
    {
        throw new NotImplementedException();
    }
    public Task Update(int id) => throw new NotImplementedException();
    #endregion
}