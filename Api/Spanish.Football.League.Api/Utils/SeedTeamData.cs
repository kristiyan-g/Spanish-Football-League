namespace Spanish.Football.League.Api.Utils
{
    using Microsoft.EntityFrameworkCore;
    using Spanish.Football.League.DomainModels;

    /// <summary>
    /// This class provides an extension method to seed data into the Teams table.
    /// </summary>
    public static class SeedTeamData
    {
        /// <summary>
        /// This method seeds the Teams data if it does not already exist.
        /// </summary>
        /// <typeparam name="T">The DbContext class.</typeparam>
        /// <param name="app">The <see cref="IApplicationBuilder"/> instance used to configure
        /// the application's request pipeline.</param>
        /// <param name="logger">An <see cref="ILogger"/> instance used to log information,
        /// warnings, and errors related to database migrations.</param>
        public static void SeedTeamsDataInDatabase<T>(this IApplicationBuilder app, ILogger logger)
            where T : DbContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<T>();
            try
            {
                if (!dbContext.Set<Team>().Any())
                {
                    // Define the teams to be inserted
                    var teams = new[]
                    {
                         new Team
                         {
                             Name = "Real Madrid",
                             Weight = 0,
                             Color = "White",
                         },
                         new Team
                         {
                             Name = "FC Barcelona",
                             Weight = 0,
                             Color = "Blue and Garnet",
                         },
                         new Team
                         {
                             Name = "Atletico Madrid",
                             Weight = 0,
                             Color = "Red and White",
                         },
                         new Team
                         {
                             Name = "Sevilla FC",
                             Weight = 0,
                             Color = "White and Red",
                         },
                         new Team
                         {
                             Name = "Real Betis",
                             Weight = 0,
                             Color = "Green and White",
                         },
                         new Team
                         {
                             Name = "Valencia CF",
                             Weight = 0,
                             Color = "White and black",
                         },
                         new Team
                         {
                             Name = "Athletic Bilbao",
                             Weight = 0,
                             Color = "Red and White",
                         },
                         new Team
                         {
                             Name = "Real Sociedad",
                             Weight = 0,
                             Color = "Blue and White",
                         },
                         new Team
                         {
                             Name = "Villarreal CF",
                             Weight = 0,
                             Color = "Yellow",
                         },
                         new Team
                         {
                             Name = "Getafe CF",
                             Weight = 0,
                             Color = "Blue",
                         },
                         new Team
                         {
                             Name = "Espanyol",
                             Weight = 0,
                             Color = "Blue and White",
                         },
                         new Team
                         {
                             Name = "Levante UD",
                             Weight = 0,
                             Color = "Clarnet and Blue",
                         },
                         new Team
                         {
                             Name = "Granada CF",
                             Weight = 0,
                             Color = "Red and White",
                         },
                         new Team
                         {
                             Name = "Celta Vigo",
                             Weight = 0,
                             Color = "Sky Blue",
                         },
                         new Team
                         {
                             Name = "Mallorca",
                             Weight = 0,
                             Color = "Red",
                         },
                         new Team
                         {
                             Name = "Alavés",
                             Weight = 0,
                             Color = "Blue and White",
                         },
                         new Team
                         {
                             Name = "Eibar",
                             Weight = 0,
                             Color = "Blue and Red",
                         },
                         new Team
                         {
                             Name = "Elche CF",
                             Weight = 0,
                             Color = "Green and White",
                         },
                         new Team
                         {
                             Name = "Rayo Vallecano",
                             Weight = 0,
                             Color = "Red and White",
                         },
                         new Team
                         {
                             Name = "Real Valladolid",
                             Weight = 0,
                             Color = "Purple and White",
                         },
                    };

                    dbContext.Set<Team>().AddRange(teams);
                    dbContext.SaveChanges();

                    logger.LogDebug("Data seeded completed successfully.");
                }
                else
                {
                    logger.LogDebug("Teams table already contains data. No seeding required.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error seeding data for context {typeof(T).Name}: {ex.Message}");
            }
        }
    }
}
