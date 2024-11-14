using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess.Configurations;

internal sealed class CourseReportEntityPartConfiguration
    : IEntityTypeConfiguration<CourseReportEntityPart>
{
    public void Configure(EntityTypeBuilder<CourseReportEntityPart> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Average).IsRequired();
        builder.Property(r => r.Perfomance).IsRequired();
        builder.Property(r => r.Course).IsRequired();
    }
}
