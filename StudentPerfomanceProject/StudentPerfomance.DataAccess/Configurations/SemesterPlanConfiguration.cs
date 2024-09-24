using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.DataAccess.Configurations;

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
	}
}
