using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;

namespace SPerfomance.DataAccess.Configurations;

public class AssignmentsConfiguration : IEntityTypeConfiguration<Assignment>
{
	public void Configure(EntityTypeBuilder<Assignment> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.EntityNumber).IsRequired();
		builder.HasIndex(x => x.EntityNumber).IsUnique();

		builder.HasOne(a => a.Discipline);

		builder.HasMany(a => a.StudentAssignments)
		.WithOne(sa => sa.Assignment)
		.IsRequired()
		.OnDelete(DeleteBehavior.Cascade);
	}
}
