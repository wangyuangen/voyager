namespace YK.Console.Core.DbContext;

public class DbInitializer(ConsoleDbContext context, ILogger<DbInitializer> logger) : IDbInitializer
{
    /// <summary>
    /// migration
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        if ((await context.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
        {
            await context.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
            logger.LogInformation("applied database migrations for console module");
        }
    }
}
