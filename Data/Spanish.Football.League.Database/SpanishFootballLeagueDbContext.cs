namespace Spanish.Football.League.Database;

using Microsoft.EntityFrameworkCore;
using Spanish.Football.League.Database.Configurations;
using Spanish.Football.League.DomainModels;

/// <summary>
/// This class is a custom Entity Framework Core database context that facilitates interaction.
/// with the database for the application.
/// </summary>
/// <param name="options">options for configuration of the database connection and other options.</param>
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
    /// Gets or sets the Seasons table.
    /// </summary>
    public DbSet<Season> Seasons { get; set; }

    /// <summary>
    /// Gets or sets the Teams table.
    /// </summary>
    public DbSet<Team> Teams { get; set; }

    /// <summary>
    /// Gets or sets the Winners table.
    /// </summary>
    public DbSet<Winner> Winners { get; set; }

    /// <summary>
    /// Gets or sets the SeasonStats table.
    /// </summary>
    public DbSet<SeasonStats> SeasonStats { get; set; }

    /// <summary>
    /// Gets or sets the TeamDetails table.
    /// </summary>
    public DbSet<TeamDetails> TeamDetails { get; set; }

    /// <summary>
    /// This method sets a default schema for the database, sets table names, apply fluent validations and
    /// any additional entity configurations defined in the assembly, ensuring all configurations are applied automatically.
    /// </summary>
    /// <param name="modelBuilder">A modelbuilder instance.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DbConstants.SchemaName);

        modelBuilder.Entity<Match>().ToTable(DbConstants.MatchesTable);
        modelBuilder.Entity<Result>().ToTable(DbConstants.ResultsTable);
        modelBuilder.Entity<Season>().ToTable(DbConstants.SeasonsTable);
        modelBuilder.Entity<Team>().ToTable(DbConstants.TeamsTable);
        modelBuilder.Entity<Winner>().ToTable(DbConstants.WinnersTable);
        modelBuilder.Entity<SeasonStats>().ToTable(DbConstants.SeasonStats);
        modelBuilder.Entity<TeamDetails>().ToTable(DbConstants.TeamDetails);

        var configurationAssembly = typeof(TeamConfiguration).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(configurationAssembly);

        base.OnModelCreating(modelBuilder);
    }
}
