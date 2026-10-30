using Microsoft.EntityFrameworkCore;
using WebMarket.DbProvider.Executors;

namespace WebMarket.DbProvider.Helpers.Extensions;

public static class DbContextExtensions
{
    /// <summary>
    /// Dapper в соединении
    /// </summary>
    public static IDbExecutor GetDbExecutor(this DbContext context) => new DbContextExecutor(context);
}
