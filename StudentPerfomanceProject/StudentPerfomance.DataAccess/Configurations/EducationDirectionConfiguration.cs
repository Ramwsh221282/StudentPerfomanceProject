using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.DataAccess.Configurations;

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
		});
		builder.OwnsOne(d => d.Type, owb =>
		{
			owb.Property(t => t.Type).HasColumnName("DirectionType").IsRequired();
		});
	}
}
