using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.DataAccess.Module.Shared.Configurations;

internal sealed class SemesterPlanConfiguration : IEntityTypeConfiguration<SemesterPlan>
{
	public void Configure(EntityTypeBuilder<SemesterPlan> builder)
	{
		builder.ToTable("SemesterPlans");
		builder.Property(p => p.EntityNumber).ValueGeneratedOnAdd();
		builder.HasKey(p => p.Id);
		builder.Property(p => p.PlanName).IsRequired();
		builder.HasOne(p => p.LinkedSemester);
		builder.HasOne(p => p.LinkedDiscipline);
		builder.HasIndex(d => d.EntityNumber).IsUnique(true);
	}
}
