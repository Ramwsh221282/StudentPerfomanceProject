using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SPerfomance.Domain.Models.TeacherDepartments;

namespace SPerfomance.DataAccess.Configurations;

internal sealed class TeachersDepartmentsConfiguration : IEntityTypeConfiguration<TeachersDepartments>
{
	public void Configure(EntityTypeBuilder<TeachersDepartments> builder)
	{
		builder.HasKey(d => d.Id);

		builder.Property(t => t.EntityNumber).ValueGeneratedOnAdd();

		builder.OwnsOne(d => d.Name, onb =>
		{
			onb.Property(n => n.Name).IsRequired();
		});

		builder.HasMany(d => d.Teachers)
		.WithOne(t => t.Department)
		.IsRequired()
		.OnDelete(DeleteBehavior.Cascade);

		builder.HasIndex(d => d.EntityNumber).IsUnique(true);
	}
}
