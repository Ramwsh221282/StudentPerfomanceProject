using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentPerfomance.DataAccess.Configurations;

internal class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroup>
{
	public void Configure(EntityTypeBuilder<StudentGroup> builder)
	{
		builder.ToTable("Groups");
		builder.HasKey(g => g.Id);
		builder.Property(g => g.EntityNumber).ValueGeneratedOnAdd();
		builder.ComplexProperty(g => g.Name, columnBuilder =>
		{
			columnBuilder.Property(g => g.Name).HasColumnName("Name").IsRequired();
		});
		builder.HasOne(g => g.EducationPlan);
		builder.HasMany(g => g.Students).WithOne(s => s.Group);
	}
}
