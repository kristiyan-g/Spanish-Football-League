namespace Spanish.Football.League.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Spanish.Football.League.DomainModels;

    public class ResultConfiguration : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.HasKey(r => r.ResultId);

            builder.Property(r => r.HomeTeamName)
                .IsRequired();

            builder.Property(r => r.AwayTeamName)
                .IsRequired();

            builder.Property(r => r.Score)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(r => r.CreatedDate)
                .IsRequired();

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
