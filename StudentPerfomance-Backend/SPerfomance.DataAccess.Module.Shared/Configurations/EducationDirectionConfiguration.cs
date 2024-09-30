using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.DataAccess.Module.Shared.Configurations;

internal sealed class EducationDirectionConfiguration : IEntityTypeConfiguration<EducationDirection>
{
	public void Configure(EntityTypeBuilder<EducationDirection> builder)
	{
		builder.HasKey(d => d.Id);
		builder.Property(d => d.EntityNumber).ValueGeneratedOnAdd();
		builder.OwnsOne(d => d.Name, owb =>
		{
			owb.Property(n => n.Name).HasColumnName("DirectionName").IsRequired();
		});
		builder.OwnsOne(d => d.Code, owb =>
		{
			owb.Property(c => c.Code).HasColumnName("DirectionCode").IsRequired();
			owb.HasIndex(c => c.Code).IsUnique(true);
		});
		builder.OwnsOne(d => d.Type, owb =>
		{
			owb.Property(t => t.Type).HasColumnName("DirectionType").IsRequired();
		});
		builder.HasIndex(d => d.EntityNumber).IsUnique(true);
	}
}
