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

		builder.Property(x => x.AssignmentOpenDate).IsRequired();
		builder.Property(x => x.AssignmentCloseDate).IsRequired(false);

		builder.ComplexProperty(x => x.Discipline, cpb =>
		{
			cpb.Property(d => d.Name).IsRequired();
		});

		builder.ComplexProperty(x => x.AssignedTo, cpb =>
		{
			cpb.Property(n => n.Name).IsRequired();
			cpb.Property(n => n.Surname).IsRequired();
			cpb.Property(n => n.Patronymic).IsRequired(false);
		});

		builder.ComplexProperty(x => x.AssignetToGroup, cpb =>
		{
			cpb.Property(g => g.Name).IsRequired();
		});

		builder.ComplexProperty(x => x.AssignedToRecordBook, cpb =>
		{
			cpb.Property(r => r.Recordbook).IsRequired();
		});

		builder.ComplexProperty(x => x.State, cpb =>
		{
			cpb.Property(s => s.State).IsRequired();
		});

		builder.OwnsOne(x => x.Assigner, cpb =>
		{
			cpb.Property(t => t!.Name).IsRequired(false);
			cpb.Property(t => t!.Surname).IsRequired(false);
			cpb.Property(t => t!.Patronymic).IsRequired(false);
		});

		builder.OwnsOne(x => x.AssignerDepartment, cpb =>
		{
			cpb.Property(d => d!.Name).IsRequired(false);
		});

		builder.OwnsOne(x => x.Value, cpb =>
		{
			cpb.Property(v => v!.Value);
		});
	}
}
