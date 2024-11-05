using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;

namespace SPerfomance.DataAccess.Configurations;

internal class AssignmentWeekConfiguration : IEntityTypeConfiguration<AssignmentWeek>
{
	public void Configure(EntityTypeBuilder<AssignmentWeek> builder)
	{
		builder.HasKey(w => w.Id);
		builder.Property(w => w.EntityNumber).IsRequired();
		builder.HasIndex(w => w.EntityNumber).IsUnique();

		builder.HasOne(w => w.Group)
		.WithMany(g => g.Weeks)
		.OnDelete(DeleteBehavior.SetNull);

		builder.HasMany(w => w.Assignments)
		.WithOne(a => a.Week)
		.IsRequired()
		.OnDelete(DeleteBehavior.Cascade);
	}
}
