using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess.Configurations;

internal class DirectionTypeReportEntityPartConfiguration
    : IEntityTypeConfiguration<DirectionTypeReportEntityPart>
{
    public void Configure(EntityTypeBuilder<DirectionTypeReportEntityPart> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.DirectionType).IsRequired();
        builder.Property(r => r.Perfomance).IsRequired();
        builder.Property(r => r.Average).IsRequired();
    }
}
