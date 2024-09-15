using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.DataAccess.Configurations;

internal sealed class SemesterConfiguration : IEntityTypeConfiguration<Semester>
{
    public void Configure(EntityTypeBuilder<Semester> builder)
    {
        builder.ToTable("Semesters");
        builder.HasKey(s => s.Id);
        builder.ComplexProperty(s => s.Number, builder =>
        {
            builder.Property(n => n.Value).IsRequired();
        });
        builder.HasOne(s => s.Group);
        builder.HasMany(s => s.Contracts).WithOne(c => c.LinkedSemester);
    }
}
