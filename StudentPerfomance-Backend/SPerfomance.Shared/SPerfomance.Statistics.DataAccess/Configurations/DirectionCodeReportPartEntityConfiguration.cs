using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess.Configurations;

internal sealed class DirectionCodeReportPartEntityConfiguration
    : IEntityTypeConfiguration<DirectionCodeReportPartEntity>
{
    public void Configure(EntityTypeBuilder<DirectionCodeReportPartEntity> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Code).IsRequired();
        builder.Property(r => r.Average).IsRequired();
        builder.Property(r => r.Perfomance).IsRequired();
    }
}
