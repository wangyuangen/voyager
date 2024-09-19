using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using YK.ORM.Abstractions;
using YK.ORM.Options;

namespace YK.ORM.Persistence;

public sealed class ConnectionStringValidator<IDbOptions>(IOptions<IDbOptions> dbSettings, ILogger<ConnectionStringValidator<IDbOptions>> logger): IConnectionStringValidator
    where IDbOptions:DatabaseOptions
{
    private readonly IDbOptions _dbSettings = dbSettings.Value;
    private readonly ILogger<ConnectionStringValidator<IDbOptions>> _logger = logger;

    public bool TryValidate(string connectionString, string? dbProvider = null)
    {
        if (string.IsNullOrWhiteSpace(dbProvider))
        {
            dbProvider = _dbSettings.Provider;
        }
        try
        {
            switch (dbProvider?.ToUpperInvariant())
            {
                case DbProviderKeys.Npgsql:
                    var postgresqlcs = new NpgsqlConnectionStringBuilder(connectionString);
                    break;

                case DbProviderKeys.MySql:
                    var mysqlcs = new MySqlConnectionStringBuilder(connectionString);
                    break;

                case DbProviderKeys.SqlServer:
                    var mssqlcs = new SqlConnectionStringBuilder(connectionString);
                    break;

                case DbProviderKeys.SqLite:
                    var sqlite = new SqliteConnectionStringBuilder(connectionString);
                    break;

                case DbProviderKeys.Oracle:
                    var oracle = new OracleConnectionStringBuilder(connectionString);
                    break;
            }

            return true;
        }
        catch (Exception ex)
        {
#pragma warning disable S6667 
            _logger.LogError("Connection String Validation Exception : {Error}", ex.Message);
#pragma warning restore S6667 
            return false;
        }
    }
}
