using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess.Configurations;

internal sealed class StudentStatisticsOnDisciplinePartEntityConfiguration
    : IEntityTypeConfiguration<StudentStatisticsOnDisciplinePartEntity>
{
    public void Configure(EntityTypeBuilder<StudentStatisticsOnDisciplinePartEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.DisciplineName).IsRequired();
    }
}
