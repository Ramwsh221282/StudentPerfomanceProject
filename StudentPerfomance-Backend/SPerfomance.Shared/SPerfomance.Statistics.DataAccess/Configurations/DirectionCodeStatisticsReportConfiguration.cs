using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess.Configurations;

internal class DirectionCodeStatisticsReportConfiguration
    : IEntityTypeConfiguration<DirectionCodeStatisticsReportEntity>
{
    public void Configure(EntityTypeBuilder<DirectionCodeStatisticsReportEntity> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Average).IsRequired();
        builder.Property(r => r.Perfomance).IsRequired();
        builder.Property(r => r.DirectionCode).IsRequired();
    }
}
