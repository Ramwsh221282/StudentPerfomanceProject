using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SPerfomance.Domain.Module.Shared.Entities.StudentGrades;

namespace SPerfomance.DataAccess.Module.Shared.Configurations;

internal sealed class StudentGradeConfiguration : IEntityTypeConfiguration<StudentGrade>
{
	public void Configure(EntityTypeBuilder<StudentGrade> builder)
	{
		builder.ToTable("Grades");
		builder.HasKey(g => g.Id);
		builder.Property(g => g.EntityNumber).ValueGeneratedOnAdd();
		builder.HasOne(g => g.Student);
		builder.HasOne(g => g.Teacher);
		builder.HasOne(g => g.Discipline);
		builder.Property(g => g.GradeDate).IsRequired();
		builder.ComplexProperty(g => g.Value, columns =>
		{
			columns.Property(g => g.Value).IsRequired();
		});
		builder.HasIndex(d => d.EntityNumber).IsUnique(true);
	}
}
