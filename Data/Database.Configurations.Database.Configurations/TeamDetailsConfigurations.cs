namespace Spanish.Football.League.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Spanish.Football.League.DomainModels;

    /// <summary>
    /// Contains fluent validations and configurations for TeamDetails entity.
    /// </summary>
    public class TeamDetailsConfigurations : IEntityTypeConfiguration<TeamDetails>
    {
        /// <summary>
        /// This method is where the configuration for the TeamDetails entity is defined.
        /// </summary>
        /// <param name="builder">The EntityTypeBuilder TeamDetails builder.</param>
        public void Configure(EntityTypeBuilder<TeamDetails> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.SeasonId)
                .IsRequired();

            builder.Property(t => t.TeamName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Weight).
                IsRequired();

            builder.Property(t => t.ExpectedWinPercentage)
                .IsRequired();

            builder.HasOne<Season>()
               .WithMany()
               .HasForeignKey(m => m.SeasonId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Team>()
                .WithMany()
                .HasPrincipalKey(t => t.Name)
                .HasForeignKey(m => m.TeamName)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
