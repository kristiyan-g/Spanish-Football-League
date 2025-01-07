namespace Spanish.Football.League.Database;

using Microsoft.EntityFrameworkCore;
using Spanish.Football.League.Database.Configurations;
using Spanish.Football.League.DomainModels;

public class SpanishFootballLeagueDbContext(DbContextOptions<SpanishFootballLeagueDbContext> options)
    : DbContext(options)
{
    /// <summary>
    /// Gets or sets the Matches table.
    /// </summary>
    public DbSet<Match> Matches { get; set; }

    /// <summary>
    /// Gets or sets the Results table.
    /// </summary>
    public DbSet<Result> Results { get; set; }

    /// <summary>
    /// Gets or sets the Teams table
    /// </summary>
    public DbSet<Team> Teams { get; set; }

    /// <summary>
    /// Gets or sets the Winners table.
    /// </summary>
    public DbSet<Winner> Winners { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DbConstants.SchemaName);

        modelBuilder.Entity<Match>().ToTable(DbConstants.MatchesTable);
        modelBuilder.Entity<Result>().ToTable(DbConstants.ResultsTable);
        modelBuilder.Entity<Team>().ToTable(DbConstants.TeamsTable).Property(b => b.TeamId);
        modelBuilder.Entity<Winner>().ToTable(DbConstants.WinnersTable);

        var configurationAssembly = typeof(TeamConfiguration).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(configurationAssembly);

        base.OnModelCreating(modelBuilder);
    }
}
