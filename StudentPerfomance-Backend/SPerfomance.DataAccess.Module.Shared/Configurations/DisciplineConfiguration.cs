using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SPerfomance.Domain.Module.Shared.Entities.Disciplines;

namespace SPerfomance.DataAccess.Module.Shared.Configurations;

internal sealed class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
{
	public void Configure(EntityTypeBuilder<Discipline> builder)
	{
		builder.ToTable("Disciplines");
		builder.HasKey(d => d.Id);
		builder.Property(d => d.EntityNumber).ValueGeneratedOnAdd();
		builder.Property(d => d.Name).IsRequired();
		builder.HasOne(d => d.Teacher)
		.WithMany(t => t.Disciplines).IsRequired(false);
		builder.HasIndex(d => d.Name);
		builder.HasIndex(d => d.EntityNumber).IsUnique(true);
	}
}
