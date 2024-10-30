using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SPerfomance.Domain.Models.EducationDirections;

namespace SPerfomance.DataAccess.Configurations;

internal sealed class EducationDirectionsConfiguration : IEntityTypeConfiguration<EducationDirection>
{
	public void Configure(EntityTypeBuilder<EducationDirection> builder)
	{
		builder.HasKey(d => d.Id);
		builder.Property(d => d.EntityNumber).ValueGeneratedOnAdd();

		builder.OwnsOne(d => d.Name, owb =>
		{
			owb.Property(n => n.Name).IsRequired();
		});

		builder.OwnsOne(d => d.Code, owb =>
		{
			owb.Property(c => c.Code).IsRequired();
			owb.HasIndex(c => c.Code).IsUnique(true);
		});

		builder.OwnsOne(d => d.Type, owb =>
		{
			owb.Property(t => t.Type).IsRequired();
		});

		builder.HasMany(d => d.Plans).WithOne(p => p.Direction)
		.IsRequired()
		.OnDelete(DeleteBehavior.Cascade);

		builder.HasIndex(d => d.EntityNumber).IsUnique(true);
	}
}
