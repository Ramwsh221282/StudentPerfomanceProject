using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.DataAccess.Module.Shared.Configurations;

internal sealed class EducationPlanConfiguration : IEntityTypeConfiguration<EducationPlan>
{
	public void Configure(EntityTypeBuilder<EducationPlan> builder)
	{
		builder.HasKey(p => p.Id);
		builder.Property(p => p.EntityNumber).ValueGeneratedOnAdd();
		builder.OwnsOne(p => p.Year, owb =>
		{
			owb.Property(y => y.Year).HasColumnName("YearOfCreation").IsRequired();
		});

		builder.HasOne(p => p.Direction);

		builder.HasMany(p => p.Groups).WithOne(g => g.EducationPlan)
		.IsRequired(false)
		.OnDelete(DeleteBehavior.SetNull);

		builder.HasMany(p => p.Semesters).WithOne(s => s.Plan)
		.IsRequired()
		.OnDelete(DeleteBehavior.Cascade);

		builder.HasIndex(d => d.EntityNumber).IsUnique(true);
	}
}
