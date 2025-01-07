namespace Spanish.Football.League.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Spanish.Football.League.DomainModels;

    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(static t => t.TeamId);

            builder.Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Weight);

            builder.Property(t => t.Color)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
