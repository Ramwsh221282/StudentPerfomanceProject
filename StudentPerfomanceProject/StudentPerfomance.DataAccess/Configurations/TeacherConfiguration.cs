using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.DataAccess.Configurations;

internal class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
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
	}
}
