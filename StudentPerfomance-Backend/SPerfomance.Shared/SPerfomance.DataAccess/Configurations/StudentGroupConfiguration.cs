using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.DataAccess.Configurations;

internal sealed class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroup>
{
	public void Configure(EntityTypeBuilder<StudentGroup> builder)
	{
		builder.HasKey(g => g.Id);
		builder.Property(g => g.EntityNumber).ValueGeneratedOnAdd();

		builder.OwnsOne(g => g.Name, columnBuilder =>
		{
			columnBuilder.Property(g => g.Name).IsRequired();
			columnBuilder.HasIndex(n => n.Name).IsUnique(true);
		});

		builder.HasOne(g => g.EducationPlan)
		.WithMany(p => p.Groups)
		.IsRequired(false)
		.OnDelete(DeleteBehavior.SetNull);

		builder.HasOne(s => s.ActiveGroupSemester)
		.WithMany()
		.IsRequired(false)
		.OnDelete(DeleteBehavior.SetNull);

		builder.HasMany(g => g.Students)
		.WithOne(s => s.AttachedGroup)
		.IsRequired()
		.OnDelete(DeleteBehavior.Cascade);

		builder.HasIndex(d => d.EntityNumber).IsUnique(true);
	}
}
