using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess.Configurations;

internal sealed class GroupStatisticsReportEntityConfiguration
    : IEntityTypeConfiguration<GroupStatisticsReportEntity>
{
    public void Configure(EntityTypeBuilder<GroupStatisticsReportEntity> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Average).IsRequired();
        builder.Property(r => r.Perfomance).IsRequired();
        builder.Property(r => r.GroupName).IsRequired();
        builder.Property(r => r.AtSemester).IsRequired();
        builder.Property(r => r.DirectionCode).IsRequired();
        builder.Property(r => r.DirectionType).IsRequired();

        builder
            .HasMany(r => r.Parts)
            .WithOne(p => p.Root)
            .HasForeignKey(p => p.RootId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
