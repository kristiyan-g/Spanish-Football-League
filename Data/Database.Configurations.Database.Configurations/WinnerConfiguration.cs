namespace Spanish.Football.League.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Spanish.Football.League.DomainModels;

    public class WinnerConfiguration : IEntityTypeConfiguration<Winner>
    {
        public void Configure(EntityTypeBuilder<Winner> builder)
        {
            builder.HasKey(w => w.WinnerId);

            builder.Property(w => w.SeasonId)
                .IsRequired();

            builder.Property(w => w.WinnerTeamName)
                .IsRequired();

            builder.Property(w => w.ExpectedWinPercentage)
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
