namespace Spanish.Football.League.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Spanish.Football.League.DomainModels;

    public class MatchConfigurations : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasKey(m => m.MatchId);

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
