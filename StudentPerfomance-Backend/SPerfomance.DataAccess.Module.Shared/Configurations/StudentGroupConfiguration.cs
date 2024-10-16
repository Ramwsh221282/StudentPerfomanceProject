using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.DataAccess.Module.Shared.Configurations;

internal sealed class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroup>
{
	public void Configure(EntityTypeBuilder<StudentGroup> builder)
	{
		builder.HasKey(g => g.Id);
		builder.Property(g => g.EntityNumber).ValueGeneratedOnAdd();

		builder.OwnsOne(g => g.Name, columnBuilder =>
		{
			columnBuilder.Property(g => g.Name).HasColumnName("Name").IsRequired();
			columnBuilder.HasIndex(n => n.Name).IsUnique(true);
		});

		builder.HasOne(g => g.EducationPlan);

		builder.HasMany(g => g.Students)
		.WithOne(s => s.Group)
		.IsRequired()
		.OnDelete(DeleteBehavior.Cascade);

		builder.HasIndex(d => d.EntityNumber).IsUnique(true);
	}
}
