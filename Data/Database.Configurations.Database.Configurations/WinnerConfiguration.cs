namespace Spanish.Football.League.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Spanish.Football.League.DomainModels;

    /// <summary>
    /// Contains fluent validations and configurations for Winner entity.
    /// </summary>
    public class WinnerConfiguration : IEntityTypeConfiguration<Winner>
    {
        /// <summary>
        /// This method is where the configuration for the Winner entity is defined.
        /// </summary>
        /// <param name="builder">The EntityTypeBuilder Winner builder.</param>
        public void Configure(EntityTypeBuilder<Winner> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Id).HasColumnName(ConfigurationsDbConstants.WinnersTableIdName);

            builder.Property(w => w.SeasonId)
                .IsRequired();

            builder.Property(w => w.WinnerTeamName)
                .IsRequired();

            builder.Property(w => w.CreatedDate)
                .IsRequired();

            builder.HasOne<Team>()
                .WithMany()
                .HasPrincipalKey(t => t.Name)
                .HasForeignKey(w => w.WinnerTeamName)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
