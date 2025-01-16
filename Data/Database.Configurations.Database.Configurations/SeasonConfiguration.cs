namespace Spanish.Football.League.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Spanish.Football.League.DomainModels;

    /// <summary>
    /// Contains fluent validations and configurations for Season entity.
    /// </summary>
    public class SeasonConfiguration : IEntityTypeConfiguration<Season>
    {
        /// <summary>
        /// This method is where the configuration for the Season entity is defined.
        /// </summary>
        /// <param name="builder">The EntityTypeBuilder Season builder.</param>
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).HasColumnName(ConfigurationsDbConstants.SeasonsTableName);

            builder.Property(s => s.SeasonYear)
                .IsRequired();

            builder.Property(s => s.CreatedDate)
                .IsRequired();
        }
    }
}
