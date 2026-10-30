using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using WebMarket.DbProvider.Executors;
using WebMarket.DbProvider.Helpers.Mappers;
using WebMarket.DbProvider.Settings.Secrets;

namespace WebMarket.DbProvider.Helpers.Extensions;

public static class DataServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует доступ к БД 
    /// </summary>
    public static IServiceCollection AddWebMarketData(this IServiceCollection services, PgSqlSecret pgSqlSecret)
    {
        services.AddSingleton(_ => NpgsqlDataSource.Create(pgSqlSecret.ToConnectionString()));

        services.AddDbContextPool<WebMarketDbContext>((provider, options) => options.UseNpgsql(provider.GetRequiredService<NpgsqlDataSource>()).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        DefaultTypeMap.MatchNamesWithUnderscores = true;
        services.AddSingleton<IDbExecutor, DbExecutor>();

        return services;
    }
}
