using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.DataAccess.Module.Shared.Configurations;

internal sealed class SemesterConfiguration : IEntityTypeConfiguration<Semester>
{
	public void Configure(EntityTypeBuilder<Semester> builder)
	{
		builder.HasKey(s => s.Id);
		builder.Property(s => s.EntityNumber).ValueGeneratedOnAdd();
		builder.ComplexProperty(s => s.Number, builder =>
		{
			builder.Property(n => n.Value).IsRequired();
		});

		builder.HasOne(s => s.Plan);

		builder
		.HasMany(s => s.Contracts)
		.WithOne(c => c.Semester)
		.IsRequired()
		.OnDelete(DeleteBehavior.Cascade);

		builder.HasIndex(d => d.EntityNumber).IsUnique(true);
	}
}
