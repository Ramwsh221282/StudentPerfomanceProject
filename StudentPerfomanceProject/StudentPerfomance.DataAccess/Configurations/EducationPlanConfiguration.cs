using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.DataAccess.Configurations;

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
		builder.HasMany(p => p.Semesters).WithOne(s => s.Plan).IsRequired();
	}
}
