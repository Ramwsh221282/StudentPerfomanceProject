using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SPerfomance.Domain.Models.Teachers;

namespace SPerfomance.DataAccess.Configurations;

internal sealed class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
	public void Configure(EntityTypeBuilder<Teacher> builder)
	{
		builder.HasKey(t => t.Id);
		builder.Property(t => t.EntityNumber).ValueGeneratedOnAdd();

		builder.HasMany(t => t.Disciplines)
		.WithOne(d => d.Teacher)
		.IsRequired(false)
		.OnDelete(DeleteBehavior.SetNull);

		builder.ComplexProperty(t => t.Name, columns =>
		{
			columns.Property(n => n.Name).IsRequired();
			columns.Property(n => n.Surname).IsRequired();
			columns.Property(n => n.Patronymic).IsRequired(false);
		});
		builder.OwnsOne(t => t.State, columns =>
		{
			columns.Property(c => c.State).IsRequired();
		});
		builder.OwnsOne(t => t.JobTitle, columns =>
		{
			columns.Property(j => j.JobTitle).IsRequired();
		});
		builder.HasIndex(d => d.EntityNumber).IsUnique(true);
	}
}
