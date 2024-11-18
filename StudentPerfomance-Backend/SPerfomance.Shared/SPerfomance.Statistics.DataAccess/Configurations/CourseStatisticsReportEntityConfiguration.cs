using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess.Configurations;

internal sealed class CourseStatisticsReportEntityConfiguration
    : IEntityTypeConfiguration<CourseStatisticsReportEntity>
{
    public void Configure(EntityTypeBuilder<CourseStatisticsReportEntity> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Average).IsRequired();
        builder.Property(r => r.Perfomance).IsRequired();
        builder.Property(r => r.Course).IsRequired();
        builder.Property(r => r.DirectionType).IsRequired();
    }
}
