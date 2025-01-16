namespace Spanish.Football.League.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Spanish.Football.League.DomainModels;

    /// <summary>
    /// Contains fluent validations and configurations for Match entity.
    /// </summary>
    public class MatchConfigurations : IEntityTypeConfiguration<Match>
    {
        /// <summary>
        /// This method is where the configuration for the Match entity is defined.
        /// </summary>
        /// <param name="builder">The EntityTypeBuilder Match builder.</param>
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id).HasColumnName(ConfigurationsDbConstants.MatchesTableIdName);

            builder.Property(m => m.SeasonId)
                .IsRequired();

            builder.Property(m => m.SeasonHalf)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(m => m.HomeTeamName)
                .IsRequired();

            builder.Property(m => m.AwayTeamName)
                .IsRequired();

            builder.Property(m => m.HomeTeamOdd)
                .HasPrecision(3, 2)
                .IsRequired();

            builder.Property(m => m.AwayTeamOdd)
                .HasPrecision(3, 2)
                .IsRequired();

            builder.Property(m => m.CreatedDate)
                .IsRequired();

            builder.HasOne<Season>()
                .WithMany()
                .HasForeignKey(m => m.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Team>()
                .WithMany()
                .HasPrincipalKey(t => t.Name)
                .HasForeignKey(m => m.HomeTeamName)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Team>()
                .WithMany()
                .HasPrincipalKey(t => t.Name)
                .HasForeignKey(m => m.AwayTeamName)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
