using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess.Configurations;

internal sealed class TeacherStatisticsReportPartEntityConfiguration
    : IEntityTypeConfiguration<TeacherStatisticsReportPartEntity>
{
    public void Configure(EntityTypeBuilder<TeacherStatisticsReportPartEntity> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.TeacherName).IsRequired();
        builder.Property(r => r.TeacherSurname).IsRequired();
        builder.Property(r => r.TeacherPatronymic).IsRequired(false);
        builder.Property(r => r.Average).IsRequired();
        builder.Property(r => r.Perfomance).IsRequired();
    }
}
