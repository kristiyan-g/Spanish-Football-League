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

            builder.Property(r => r.HomeTeamId)
                .IsRequired();

            builder.Property(r => r.AwayTeamId)
                .IsRequired();

            builder.Property(r => r.Score)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(r => r.CreatedDate)
                .IsRequired();

            builder.HasOne<Team>()
                .WithMany()
                .HasForeignKey(r => r.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Team>()
                .WithMany()
                .HasForeignKey(r => r.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
