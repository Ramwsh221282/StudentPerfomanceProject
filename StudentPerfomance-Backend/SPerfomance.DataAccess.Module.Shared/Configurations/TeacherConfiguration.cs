using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.DataAccess.Module.Shared.Configurations;

internal sealed class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
	public void Configure(EntityTypeBuilder<Teacher> builder)
	{
		builder.ToTable("Teachers");
		builder.HasKey(t => t.Id);
		builder.Property(t => t.EntityNumber).ValueGeneratedOnAdd();
		builder.HasMany(t => t.Disciplines).WithOne(d => d.Teacher).IsRequired(false);
		builder.HasOne(t => t.Department).WithMany(d => d.Teachers).IsRequired();
		builder.ComplexProperty(t => t.Name, columns =>
		{
			columns.Property(n => n.Name).HasColumnName("Name").IsRequired();
			columns.Property(n => n.Surname).HasColumnName("Surname").IsRequired();
			columns.Property(n => n.Thirdname).HasColumnName("Thirdname").IsRequired(false);
		});
		builder.OwnsOne(t => t.Condition, columns =>
		{
			columns.Property(c => c.Value).HasColumnName("WorkingCondition").IsRequired();
		});
		builder.OwnsOne(t => t.JobTitle, columns =>
		{
			columns.Property(j => j.Value).HasColumnName("JobTitle").IsRequired();
		});
		builder.HasIndex(d => d.EntityNumber).IsUnique(true);
	}
}
