using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess.Configurations;

internal class StudentStatisticsReportEntityConfiguration
    : IEntityTypeConfiguration<StudentStatisticsReportEntity>
{
    public void Configure(EntityTypeBuilder<StudentStatisticsReportEntity> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Average).IsRequired();
        builder.Property(r => r.Perfomance).IsRequired();
        builder.Property(r => r.StudentName).IsRequired();
        builder.Property(r => r.StudentSurname).IsRequired();
        builder.Property(r => r.StudentPatronymic);
        builder.Property(r => r.Grade).IsRequired();
        builder.Property(r => r.Recordbook).IsRequired();
    }
}
