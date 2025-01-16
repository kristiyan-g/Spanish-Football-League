namespace Spanish.Football.League.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Spanish.Football.League.DomainModels;

    /// <summary>
    /// Contains fluent validations and configurations for SeasonStats entity.
    /// </summary>
    public class SeasonStatsConfiguration : IEntityTypeConfiguration<SeasonStats>
    {
        /// <summary>
        /// This method is where the configuration for the SeasonStats entity is defined.
        /// </summary>
        /// <param name="builder">The EntityTypeBuilder SeasonStats builder.</param>
        public void Configure(EntityTypeBuilder<SeasonStats> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.SeasonId)
                .IsRequired();

            builder.Property(t => t.TeamName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.ScoredGoals)
                .IsRequired();

            builder.Property(t => t.ConcededGoals)
                .IsRequired();

            builder.Property(t => t.GoalDifference)
                .IsRequired();

            builder.Property(t => t.Points)
                .IsRequired();

            builder.Property(t => t.Wins)
                .IsRequired();

            builder.Property(t => t.Draws)
                .IsRequired();

            builder.Property(t => t.Losses)
                .IsRequired();

            builder.HasOne<Season>()
                .WithMany()
                .HasForeignKey(t => t.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Team>()
                .WithMany()
                .HasPrincipalKey(t => t.Name)
                .HasForeignKey(t => t.TeamName)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
