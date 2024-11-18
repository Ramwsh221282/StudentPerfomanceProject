using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess.Configurations;

internal sealed class DisciplineStatisticsReportEntityConfiguration
    : IEntityTypeConfiguration<DisciplinesStatisticsReportEntity>
{
    public void Configure(EntityTypeBuilder<DisciplinesStatisticsReportEntity> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.DisciplineName).IsRequired();
        builder.Property(r => r.TeacherName).IsRequired();
        builder.Property(r => r.TeacherSurname).IsRequired();
        builder.Property(r => r.TeacherPatronymic);
        builder.Property(r => r.Average).IsRequired();
        builder.Property(r => r.Perfomance).IsRequired();

        builder
            .HasMany(r => r.Parts)
            .WithOne(p => p.Root)
            .HasForeignKey(p => p.RootId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
