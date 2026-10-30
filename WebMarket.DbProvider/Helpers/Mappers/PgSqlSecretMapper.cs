using System.Reflection;
using Npgsql;
using WebMarket.DbProvider.Settings.Secrets;

namespace WebMarket.DbProvider.Helpers.Mappers;

public static class PgSqlSecretMapper
{
    /// <summary>
    /// Собирает строку подключения к PostgreSQL из секрета.
    /// </summary>
    public static string ToConnectionString(this PgSqlSecret secret) => new NpgsqlConnectionStringBuilder
    {
        Host = secret.Host,
        Port = secret.Port,
        Database = secret.Database,
        Username = secret.Username,
        Password = secret.Password,
        ApplicationName = Assembly.GetEntryAssembly()?.GetName().Name
    }.ConnectionString;
}
