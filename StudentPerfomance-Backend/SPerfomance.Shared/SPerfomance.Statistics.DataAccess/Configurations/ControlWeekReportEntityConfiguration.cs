using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess.Configurations;

internal sealed class ControlWeekReportEntityConfiguration
    : IEntityTypeConfiguration<ControlWeekReportEntity>
{
    public void Configure(EntityTypeBuilder<ControlWeekReportEntity> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.RowNumber).ValueGeneratedOnAdd();
        builder.Property(r => r.CreationDate).IsRequired();
        builder.Property(r => r.CompletionDate).IsRequired();
        builder.Property(r => r.ControlWeekNumber).IsRequired();
        builder.Property(r => r.ControlWeekSeason).IsRequired();

        builder
            .HasMany(r => r.GroupParts)
            .WithOne(r => r.Root)
            .HasForeignKey(r => r.RootId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(r => r.CourseParts)
            .WithOne(r => r.Root)
            .HasForeignKey(r => r.RootId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(r => r.DirectionCodeReport)
            .WithOne(r => r.Root)
            .HasForeignKey(r => r.RootId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(r => r.DirectionTypeReport)
            .WithOne(r => r.Root)
            .HasForeignKey(r => r.RootId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
