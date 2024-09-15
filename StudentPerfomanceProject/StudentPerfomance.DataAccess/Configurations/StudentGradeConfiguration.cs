using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.DataAccess.Configurations;

public class StudentGradeConfiguration : IEntityTypeConfiguration<StudentGrade>
{
    public void Configure(EntityTypeBuilder<StudentGrade> builder)
    {
        builder.ToTable("Grades");
        builder.HasKey(g => g.Id);
        builder.HasOne(g => g.Student);
        builder.HasOne(g => g.Teacher);
        builder.HasOne(g => g.Discipline);
        builder.Property(g => g.GradeDate).IsRequired();
        builder.ComplexProperty(g => g.Value, columns => 
        {
            columns.Property(g => g.Value).IsRequired();
        });        
    }
}
