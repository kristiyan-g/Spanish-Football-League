namespace Spanish.Football.League.Api.Utils
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// This class provides an extension method, to apply pending database migrations at runtime.
    /// </summary>
    public static class EnsureMigrations
    {
        /// <summary>
        /// The method, takes a DbContext type (T) as a parameter and applies any pending migrations
        /// for that context when the application starts.
        /// </summary>
        /// <typeparam name="T">The DbContext class.</typeparam>
        /// <param name="app">The <see cref="IApplicationBuilder"/> instance used to configure
        /// the application's request pipeline.</param>
        /// <param name="logger">An <see cref="ILogger"/> instance used to log information,
        /// warnings, and errors related to database migrations.</param>
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app, ILogger logger)
        where T : DbContext
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<T>();
                try
                {
                    var pendingMigrations = dbContext.Database.GetPendingMigrations();
                    if (pendingMigrations.Any())
                    {
                        dbContext.Database.Migrate();
                        logger.LogInformation($"Database migration completed successfully for context {typeof(T).Name}.");
                    }
                    else
                    {
                        logger.LogInformation($"No pending migrations for context {typeof(T).Name}.");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"Error applying migrations for context {typeof(T).Name}: {ex.Message}");
                }
            }
        }
    }
}
