using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess.Configurations;

internal sealed class StudentStatisticsPartEntityConfiguration
    : IEntityTypeConfiguration<StudentStatisticsPartEntity>
{
    public void Configure(EntityTypeBuilder<StudentStatisticsPartEntity> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Perfomance).IsRequired();
        builder.Property(r => r.Average).IsRequired();
        builder.Property(r => r.StudentName).IsRequired();
        builder.Property(r => r.StudentSurname).IsRequired();
        builder.Property(r => r.StudentPatronymic).IsRequired(false);
        builder.Property(r => r.StudentRecordBook).IsRequired();

        builder
            .HasMany(r => r.Parts)
            .WithOne(p => p.Root)
            .HasForeignKey(p => p.RootId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
