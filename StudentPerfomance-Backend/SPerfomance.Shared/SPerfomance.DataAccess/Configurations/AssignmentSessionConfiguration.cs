using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;

namespace SPerfomance.DataAccess.Configurations;

internal sealed class AssignmentSessionConfiguration : IEntityTypeConfiguration<AssignmentSession>
{
	public void Configure(EntityTypeBuilder<AssignmentSession> builder)
	{
		builder.HasKey(s => s.Id);
		builder.Property(s => s.EntityNumber).IsRequired();
		builder.HasIndex(s => s.EntityNumber).IsUnique();

		builder.Property(s => s.SessionStartDate).IsRequired();
		builder.Property(s => s.SessionCloseDate).IsRequired();

		builder.ComplexProperty(s => s.State, cpb =>
		{
			cpb.Property(v => v.State).IsRequired();
		});

		builder.HasMany(s => s.Weeks)
		.WithOne(w => w.Session)
		.IsRequired()
		.OnDelete(DeleteBehavior.Cascade);
	}
}
