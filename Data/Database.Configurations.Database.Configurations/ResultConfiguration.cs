namespace Spanish.Football.League.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Spanish.Football.League.DomainModels;

    /// <summary>
    /// Contains fluent validations and configurations for Result entity.
    /// </summary>
    public class ResultConfiguration : IEntityTypeConfiguration<Result>
    {
        /// <summary>
        /// This method is where the configuration for the Result entity is defined.
        /// </summary>
        /// <param name="builder">The EntityTypeBuilder Result builder.</param>
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id).HasColumnName(ConfigurationsDbConstants.ResultsTableIdName);

            builder.Property(r => r.SeasonId)
                .IsRequired();

            builder.Property(r => r.SeasonHalf)
                .IsRequired();

            builder.Property(r => r.HomeTeamName)
                .IsRequired();

            builder.Property(r => r.AwayTeamName)
                .IsRequired();

            builder.Property(r => r.HomeTeamScore)
                .IsRequired();

            builder.Property(r => r.AwayTeamScore)
                .IsRequired();

            builder.Property(r => r.CreatedDate)
                .IsRequired();

            builder.HasOne<Season>()
                .WithMany()
                .HasForeignKey(m => m.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Team>()
                .WithMany()
                .HasPrincipalKey(t => t.Name)
                .HasForeignKey(r => r.HomeTeamName)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Team>()
                .WithMany()
                .HasPrincipalKey(t => t.Name)
                .HasForeignKey(r => r.AwayTeamName)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
