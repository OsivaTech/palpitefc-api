﻿using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using PalpiteApi.Infra.Persistence.Settings;
using System.Data;

namespace PalpiteApi.Infra.Persistence.Connection;

public sealed class DbSession : IDisposable
{
    #region Fields

    public IDbConnection Connection { get; }
    public IDbTransaction Transaction { get; set; }

    #endregion

    #region Constructors

    public DbSession(IOptions<DbSettings> dbSettings)
    {
        Connection = new MySqlConnection(dbSettings.Value.ToConnectionString());
        Connection.Open();
    }

    #endregion

    #region Public Methods

    public void Dispose() => Connection?.Dispose();

    #endregion
}