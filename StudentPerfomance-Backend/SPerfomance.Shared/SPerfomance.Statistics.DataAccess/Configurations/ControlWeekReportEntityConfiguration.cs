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
        builder.Property(r => r.IsFinished).IsRequired();

        builder
            .HasMany(r => r.GroupParts)
            .WithOne(r => r.Root)
            .HasForeignKey(r => r.RootId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(r => r.CourseReport)
            .WithOne(p => p.Root)
            .HasForeignKey<CourseReportEntity>(p => p.RootId);

        builder
            .HasOne(r => r.DirectionTypeReport)
            .WithOne(p => p.Root)
            .HasForeignKey<DirectionTypeReportEntity>(p => p.RootId);

        builder
            .HasOne(r => r.DirectionCodeReport)
            .WithOne(p => p.Root)
            .HasForeignKey<DirectionCodeReportEntity>(p => p.RootId);

        builder
            .HasOne(r => r.DepartmentReport)
            .WithOne(p => p.Root)
            .HasForeignKey<DepartmentStatisticsReportEntity>(p => p.RootId);
    }
}
