using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.DataAccess.Module.Shared.Configurations;

internal sealed class TeachersDepartmentConfiguration : IEntityTypeConfiguration<TeachersDepartment>
{
	public void Configure(EntityTypeBuilder<TeachersDepartment> builder)
	{
		builder.HasKey(d => d.Id);

		builder.Property(t => t.EntityNumber).ValueGeneratedOnAdd();

		builder.Property(d => d.FullName).IsRequired();

		builder.HasMany(d => d.Teachers)
		.WithOne(t => t.Department)
		.IsRequired()
		.OnDelete(DeleteBehavior.Cascade);

		builder.HasIndex(d => d.FullName).IsUnique();

		builder.HasIndex(d => d.EntityNumber).IsUnique(true);
	}
}
