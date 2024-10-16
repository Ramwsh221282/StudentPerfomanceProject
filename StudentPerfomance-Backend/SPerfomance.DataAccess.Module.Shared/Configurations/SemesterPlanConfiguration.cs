using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.DataAccess.Module.Shared.Configurations;

internal sealed class SemesterPlanConfiguration : IEntityTypeConfiguration<SemesterPlan>
{
	public void Configure(EntityTypeBuilder<SemesterPlan> builder)
	{
		builder.Property(p => p.EntityNumber).ValueGeneratedOnAdd();
		builder.HasKey(p => p.Id);
		builder.OwnsOne(p => p.Discipline, onv =>
		{
			onv.Property(d => d.Name);
		});
		builder.HasOne(p => p.Semester);
		builder.HasIndex(d => d.EntityNumber).IsUnique(true);
	}
}
